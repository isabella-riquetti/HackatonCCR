using HackathonCCR.EDM.Models;
using HackathonCCR.EDM.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace HackathonCCR.MVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<SelectListItem> GetCategorySelectList()
        {
            var categories = _unitOfWork.RepositoryBase.GetIQueryable<Category>();
            var selectList = categories.Select(c => new SelectListItem()
            {
                Text = c.Description,
                Value = c.CategoryId.ToString()
            }).ToList();
            return selectList;
        }
    }
}
