using System;
using System.Collections.Generic;
using System.Text;

namespace DrAgenda.Core.Dominio.Operacional
{
    public enum StatusConsulta
    {
        Agendada = 0,
        Realizada = 1,
        PendentePagamento = 2,
        PagamentoOk = 3,
        Cancelada = 4
    }
}
