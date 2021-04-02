using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrAgenda.Shared
{
    public enum DiaSemana
    {
        [Display(Name = "Domingo")]
        [Description("Domingo")]
        Domingo = 0,

        [Display(Name = "Segunda-Feira")]
        [Description("Segunda-Feira")]
        Segunda = 1,

        [Display(Name = "Terça-Feira")]
        [Description("Terça-Feira")]
        Terca = 2,

        [Display(Name = "Quarta-Feira")]
        [Description("Quarta-Feira")]
        Quarta = 3,

        [Display(Name = "Quinta")]
        [Description("Quinta-Feira")]
        Quinta = 4,

        [Display(Name = "Sexta-Feira")]
        [Description("Sexta-Feira")]
        Sexta = 5,

        [Display(Name = "Sabado")]
        [Description("Sábado")]
        Sabado = 6,

        [Display(Name = "Todos os Dias")]
        [Description("Todos os Dias")]
        TodosDias = 7

    }
}