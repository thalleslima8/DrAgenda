using System;
using Codout.Framework.DAL.Entity;
using DrAgenda.Core.Dominio.Base.Interface;

namespace DrAgenda.Data.ORM.Base
{
    public class AuditClassMapBase<T> : ClassMapBase<T> where T : IEntity<Guid?>, IEntity, IAudit
    {
        public AuditClassMapBase(string tableName)
            : base(tableName)
        {
            Map(x => x.DataCriacao);
            Map(x => x.DataAtualizacao);
            Map(x => x.CriadoPor);
            Map(x => x.AtualizadoPor);
        }
    }
}
