using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Helper;
using HackathonCCR.MVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace HackathonCCR.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor accessor;

        public AuthenticationService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public bool ConfirmLogin(User user, LoginModel model)
        {
            if (user == null || user.Password != CryptHelper.Encrypt(model.Password))
                return false;

            return true;
        }
        public Guid GetAuthenticatedUserId()
        {
            var user = accessor?.HttpContext?.User;
            var userIdString = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userId = new Guid(userIdString);
            return userId;
        }
    }
}
