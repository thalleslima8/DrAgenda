using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrAgenda.Shared
{
    public enum StatusPaciente
    {
        [Display(Name = "Ativo")]
        [Description("Ativo")]
        Ativo = 0,

        [Display(Name = "Inativo")]
        [Description("Inativo")]
        Inativo = 1,

        [Display(Name = "Pausado")]
        [Description("Pausado")]
        Pausado = 2,

        [Display(Name = "Bloqueado")]
        [Description("Bloqueado")]
        Bloqueado = 3,

        [Display(Name = "Inadimplente")]
        [Description("Inadimplente")]
        Inadimplente = 4,

        [Display(Name = "Finalizado")]
        [Description("Finalizado")]
        Finalizado = 5
    }
}
