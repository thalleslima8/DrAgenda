using System;
using DrAgenda.Core.Dominio.Base.Interface;

namespace DrAgenda.Core.Dominio.Base
{
    [Serializable]
    public abstract class EntityAudit : Entity, IAudit
    {
        public virtual DateTime? DataCriacao { get; set; }
        public virtual DateTime? DataAtualizacao { get; set; }
        public virtual string CriadoPor { get; set; }
        public virtual string AtualizadoPor { get; set; }
    }
}
