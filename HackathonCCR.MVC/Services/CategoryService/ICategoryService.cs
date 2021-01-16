using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HackathonCCR.MVC.Services
{
    public interface ICategoryService
    {
        List<SelectListItem> GetCategorySelectList();
    }
}
