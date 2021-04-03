using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DrAgenda.Core.Dominio.Base.Interface;
using DrAgenda.Data.Helpers;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;

namespace DrAgenda.Data
{
    public class DrAgendaListener : DefaultDeleteEventListener, IPreInsertEventListener, IPreUpdateEventListener
    {
        public async Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return await Task.Run(() => OnPreInsert(@event), cancellationToken);
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            if (@event.Entity is IAudit audit)
            {
                var time = DateTime.Now;
                var name = Local.Data?["Username"]?.ToString(); 

                Set(@event.Persister, @event.State, "DataCriacao", time);
                Set(@event.Persister, @event.State, "DataAtualizacao", time);
                Set(@event.Persister, @event.State, "CriadoPor", name);
                Set(@event.Persister, @event.State, "AtualizadoPor", name);

                audit.DataCriacao = time;
                audit.CriadoPor = name;
                audit.DataAtualizacao = time;
                audit.AtualizadoPor = name;
            }

            if (@event.Entity is ISequencial codeSequence)
            {
                if (@event.Persister is SingleTableEntityPersister entityName)
                {
                    var nextCode = @event.Session
                        .CreateSQLQuery($"SELECT ISNULL(MAX(Sequencial),0) FROM {entityName.TableName}")
                        .UniqueResult<long>() + 1;

                    Set(@event.Persister, @event.State, "Sequencial", nextCode);

                    codeSequence.Sequencial = nextCode;
                }
            }

            return false;
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            throw new System.NotImplementedException();
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }

        protected override void DeleteEntity(IEventSource session, object entity, EntityEntry entityEntry, bool isCascadeDeleteEnabled, IEntityPersister persister, ISet<object> transientEntities)
        {
            if ( entity is ISoftDeletable e )
            {
                e.DeletedAt = DateTime.Now;
                e.IsDeleted = true;
 
                CascadeBeforeDelete( session, persister, e, entityEntry, transientEntities );
                CascadeAfterDelete( session, persister, e, transientEntities );
            }
            else
            {
                base.DeleteEntity( session, entity, entityEntry, isCascadeDeleteEnabled, persister, transientEntities);
            }
        }
    }
}
