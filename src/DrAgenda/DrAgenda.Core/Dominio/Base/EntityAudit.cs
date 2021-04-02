using System;
using DrAgenda.Core.Dominio.Base.Interface;

namespace DrAgenda.Core.Dominio.Base
{
    [Serializable]
    public abstract class EntityAudit : Entity, IAudit
    {
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public string CriadoPor { get; set; }
        public string AtualizadoPor { get; set; }
    }
}
