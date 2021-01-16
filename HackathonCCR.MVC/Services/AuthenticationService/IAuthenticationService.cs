using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Models;

namespace HackathonCCR.MVC.Services
{
    public interface IAuthenticationService
    {
        bool ConfirmLogin(User user, LoginModel model);
    }
}
