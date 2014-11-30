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
            var url = string.Format("/admin/uploads/{0}/{1}/{2}/{3}",
                User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month, fileName);

            var dir = string.Format(@"wwwroot\admin\uploads\{0}\{1}\{2}",
                User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month);

            dir = Path.Combine(_appEnvironment.ApplicationBasePath, dir);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var path = string.Format(@"wwwroot\admin\uploads\{0}\{1}\{2}\{3}",
                User.Identity.Name, DateTime.Now.Year, DateTime.Now.Month, fileName);

            path = Path.Combine(_appEnvironment.ApplicationBasePath, path);

            Stream bodyStream = Context.Request.Body;
            using (FileStream fileStream = System.IO.File.Create(path))
            {
                bodyStream.CopyTo(fileStream);
            }
            return url;
        }

    }
}