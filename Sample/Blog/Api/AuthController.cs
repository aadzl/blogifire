using Microsoft.AspNet.Mvc;

namespace BlogiFire.Api
{
    [Route("blog/api/[controller]")]
    public class AuthController : Controller
    {
        // GET: blog/api/auth
        // used to allow JavaScript check for authentication
        [HttpGet]
        public string Get()
        {
            return User.Identity.IsAuthenticated ? User.Identity.Name : "anonymous";
        }

    }
}