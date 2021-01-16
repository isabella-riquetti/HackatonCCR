using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Models;
using System;

namespace HackathonCCR.MVC.Services
{
    public interface IUserService
    {
        User Get(string email);
        User Get(Guid id);
        User Register(RegisterDiscoverModel model);
        User Register(RegisterMentorModel model);
    }
}
