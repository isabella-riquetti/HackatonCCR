using System;
using System.ComponentModel.DataAnnotations;

namespace HackathonCCR.MVC.Models
{
    public class RegisterMentorModel : RegisterDiscoverModel
    {
        [Display(Name = "Graduação")]
        [Required(ErrorMessage = "A '{0}' é obrigatória")]
        public Guid GraduationId { get; set; }

        [Display(Name = "Horas complementares restantes")]
        [Required(ErrorMessage = "As '{0}' são obrigatórias. Insira 0 caso não tenha.")]
        [Range(0, int.MaxValue, ErrorMessage = "As '{0}' deve ser um valor entre {1} e {2}")]
        public int RemainingMissingHours { get; set; }

        [Display(Name = "Cargo atual (opcional)")]
        [StringLength(200, ErrorMessage = "A '{0}' deve ser um valor entre {1} e {2}")]
        public string WorkingField { get; set; }

        public EDM.Enums.User.Type Type = EDM.Enums.User.Type.Mentor;
    }
}
