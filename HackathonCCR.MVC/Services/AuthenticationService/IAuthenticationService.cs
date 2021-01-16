using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Models;
using System;

namespace HackathonCCR.MVC.Services
{
    public interface IAuthenticationService
    {
        bool ConfirmLogin(User user, LoginModel model);
        Guid GetAuthenticatedUserId();
    }
}
