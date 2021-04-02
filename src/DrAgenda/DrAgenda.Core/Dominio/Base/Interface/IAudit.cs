using System;

namespace DrAgenda.Core.Dominio.Base.Interface
{
    public interface IAudit
    {
        DateTime? DataCriacao { get; set; } 
        DateTime? DataAtualizacao { get; set; }
        string CriadoPor { get; set; }
        string AtualizadoPor { get; set; }
    }
}
