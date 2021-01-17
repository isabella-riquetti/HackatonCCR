using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;
using HackathonCCR.MVC.Helper;
using HackathonCCR.MVC.Models;
using System;
using System.IO;

namespace HackathonCCR.MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        public UserService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public User Get(string email)
        {
            var user = _unitOfWork.RepositoryBase.FirstOrDefault<User>(u => u.Email == email);
            return user;
        }

        public User Get(Guid id)
        {
            var user = _unitOfWork.RepositoryBase.FirstOrDefault<User>(u => u.UserId == id);
            return user;
        }

        public User Register(RegisterDiscoverModel model)
        {
            byte[] picture;
            using (var ms = new MemoryStream())
            {
                model.Picture.CopyTo(ms);
                picture = ms.ToArray();
            }

            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = model.Email,
                Name = model.Name,
                Password = CryptHelper.Encrypt(model.Password),
                Type = model.Type,
                PhoneNumber = model.PhoneNumber,
                Picture = picture
            };
            _unitOfWork.RepositoryBase.Add(user);
            _unitOfWork.Commit();

            return user;
        }

        public User Register(RegisterMentorModel model)
        {
            byte[] picture;
            using (var ms = new MemoryStream())
            {
                model.Picture.CopyTo(ms);
                picture = ms.ToArray();
            }
            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = model.Email,
                Name = model.Name,
                Password = CryptHelper.Encrypt(model.Password),
                Type = model.Type,
                PhoneNumber = model.PhoneNumber,
                GraduationId = model.GraduationId,
                WorkingField = model.WorkingField,
                RemainingMissingHours = model.RemainingMissingHours,
                Picture = picture
            };
            _unitOfWork.RepositoryBase.Add(user);
            _unitOfWork.Commit();

            return user;
        }

        public string GetUserPicure()
        {
            var userId = _authenticationService.GetAuthenticatedUserId();
            var user = _unitOfWork.RepositoryBase.FirstOrDefault<User>(u => u.UserId == userId);
            var picture = user.Picture != null && user.Picture.Length > 0 ? Convert.ToBase64String(user.Picture) : null;
            return picture;
        }
    }
}
