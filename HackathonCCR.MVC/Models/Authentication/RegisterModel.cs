using HackathonCCR.EDM.Models;
using System.ComponentModel.DataAnnotations;

namespace HackathonCCR.MVC.Models
{
    public class RegisterModel : User
    {
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da senha")]
        [Required(ErrorMessage = "A '{0}' é obrigatória")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public EDM.Enums.User.Type Type { get; set; }
    }
}
