using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Helper;
using HackathonCCR.MVC.Models;

namespace HackathonCCR.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool ConfirmLogin(User user, LoginModel model)
        {
            if (user == null || Crypt.Encrypt(user.Password) != model.Password)
                return false;

            return true;
        }
    }
}
