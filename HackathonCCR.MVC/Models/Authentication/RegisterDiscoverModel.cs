using System;
using System.ComponentModel.DataAnnotations;

namespace HackathonCCR.MVC.Models
{
    public class RegisterDiscoverModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "O '{0}' não é um e-mail válido")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A '{0}' é obrigatória")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "A senha deve conter ao menos uma letra maiúscula, uma letra minúscula e um número")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação da senha")]
        [Required(ErrorMessage = "A '{0}' é obrigatória")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        [RegularExpression(@"^(\(?[0]?[1-9][0-9]\)?)(\.|\-|\s)?([9]{1})?[6-9][0-9]{3}(\.|\-|\s)?[0-9]{4}$", ErrorMessage = "O '{0}' deve estar no formato: (XX) X XXXX-XXXX")]
        public string PhoneNumber { get; set; }

        public EDM.Enums.User.Type Type = EDM.Enums.User.Type.Discover;
    }
}
