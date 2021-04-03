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
    
}
