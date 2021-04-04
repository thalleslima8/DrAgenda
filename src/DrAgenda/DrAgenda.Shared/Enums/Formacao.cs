using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrAgenda.Shared.Enums
{
    public enum Formacao
    {
        [Display(Name = "Enfermeiro")]
        [Description("Enfermeiro")]
        Enfermeiro = 0,

        [Display(Name = "Cardiologista")]
        [Description("Cardiologista")]
        Cardiologista = 1,

        [Display(Name = "Psicologo")]
        [Description("Psicologo")]
        Psicologo = 2,

        [Display(Name = "Neurologista")]
        [Description("Neurologista")]
        Neurologista = 3,

        [Display(Name = "Endocrinologista")]
        [Description("Endocrinologista")]
        Endocrinologista = 4,

        [Display(Name = "Fisioterapeuta")]
        [Description("Fisioterapeuta")]
        Fisioterapeuta = 5,

        [Display(Name = "Nutricionista")]
        [Description("Nutricionista")]
        Nutricionista = 6,

        [Display(Name = "Dentista")]
        [Description("Dentista")]
        Dentista = 7

    }
}
