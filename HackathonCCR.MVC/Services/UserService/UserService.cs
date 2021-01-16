using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;
using HackathonCCR.MVC.Helper;
using HackathonCCR.MVC.Models;
using System;

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

        public User Register(RegisterModel model)
        {
            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = model.Email,
                Name = model.Name,
                Password = Crypt.Encrypt(model.Password),
                Type = model.Type
            };
            _unitOfWork.RepositoryBase.Add<User>(user);
            _unitOfWork.Commit();

            return user;
        }
    }
}
