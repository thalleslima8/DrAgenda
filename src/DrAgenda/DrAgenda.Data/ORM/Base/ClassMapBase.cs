using System;
using Codout.Framework.DAL.Entity;
using DrAgenda.Core.Dominio.Base.Interface;
using FluentNHibernate.Mapping;

namespace DrAgenda.Data.ORM.Base
{
    public class ClassMapBase<T> : ClassMap<T> where T : IEntity<Guid?>, IEntity
    {
        public ClassMapBase(string tableName)
        {
            Table(tableName);

            Id(x => x.Id).GeneratedBy.Guid();
        }
    }

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
