using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;

namespace HackathonCCR.MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetUser(object email)
        {
            var user = _unitOfWork.RepositoryBase.FirstOrDefault<User>(u => u.Email == email);
            return user;
        }
    }
}
