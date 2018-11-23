using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [Route("home")]
    [Controller]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetUserDetails()
        {
            return new ObjectResult(new
            {
                Username = User.Identity.Name
            });
        }
    }
}
