using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Models;

namespace HackathonCCR.MVC.Services
{
    public interface IUserService
    {
        User GetUser(object email);
        User Register(RegisterModel model);
    }
}
