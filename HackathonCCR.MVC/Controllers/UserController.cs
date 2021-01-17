using HackathonCCR.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HackathonCCR.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public JsonResult GetUserPicture()
        {
            var picture = _userService.GetUserPicure();
            return Json(picture, new JsonSerializerOptions());
        }
    }
}
