using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrAgenda.Shared
{
    public enum StatusConsulta
    {
        [Display(Name = "Agendada")]
        [Description("Agendada")]
        Agendada = 0,

        [Display(Name = "Realizada")]
        [Description("Realizada")]
        Realizada = 1,

        [Display(Name = "PendentePagamento")]
        [Description("PendentePagamento")]
        PendentePagamento = 2,

        [Display(Name = "PagamentoOk")]
        [Description("PagamentoOk")]
        PagamentoOk = 3,

        [Display(Name = "Cancelada")]
        [Description("Cancelada")]
        Cancelada = 4
    }
}
