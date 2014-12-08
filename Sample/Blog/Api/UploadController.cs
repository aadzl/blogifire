using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Runtime;
using System;
using System.IO;

namespace BlogiFire.Api
{
    public class UploadController : Controller
    {
        private readonly IApplicationEnvironment _appEnvironment;

        public UploadController(IApplicationEnvironment appEvironment)
        {
            _appEnvironment = appEvironment;
        }

        [Route("blog/api/upload/{fileName}")]
        [Authorize]
        public string Post(string fileName)
        {
            /*
            in hosting environment, app deployed to "\site\approot\appname"
            whith webroot in "\site\wwwroot" - get web root here
            */
            var appName = _appEnvironment.ApplicationName;
            var webRoot = _appEnvironment.ApplicationBasePath.Replace(@"approot\src\" + appName, "");

            var url = string.Format("uploads/{0}/{1}/{2}/{3}",
                User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month, fileName);

            var dir = string.Format(@"wwwroot\admin\uploads\{0}\{1}\{2}",
                User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month);

            dir = Path.Combine(webRoot, dir);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var path = Path.Combine(dir, fileName);

            Stream bodyStream = Context.Request.Body;
            using (FileStream fileStream = System.IO.File.Create(path))
            {
                bodyStream.CopyTo(fileStream);
            }
            return url;
        }

    }
}