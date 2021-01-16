using HackathonCCR.EDM.Models;

namespace HackathonCCR.MVC.Services
{
    public interface IUserService
    {
        User GetUser(object email);
    }
}
