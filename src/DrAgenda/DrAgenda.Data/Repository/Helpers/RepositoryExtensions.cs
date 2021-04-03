using System;
using System.Threading.Tasks;
using Codout.Framework.DAL.Entity;
using Codout.Framework.DAL.Repository;

namespace DrAgenda.Data.Repository.Helpers
{
    public static class RepositoryExtensions
    {
        public static TDomain GetOrCreateInstance<TDomain>(this IRepository<TDomain> repository, Guid? id)
            where TDomain : class, IEntity<Guid?>, new()
        {
            return id.HasValue ? repository.Get(id) ?? new TDomain() : new TDomain();
        }

        public static TDomain LoadOrDefault<TDomain>(this IRepository<TDomain> repository, Guid? id)
            where TDomain : class, IEntity<Guid?>, new()
        {
            return id.HasValue ? repository.Load(id) : null;
        }

        public static async Task<TDomain> GetOrCreateInstanceAsync<TDomain>(this IRepository<TDomain> repository, Guid? id)
            where TDomain : class, IEntity<Guid?>, new()
        {
            return id.HasValue ? await repository.GetAsync(id) ?? new TDomain() : new TDomain();
        }

        public static async Task<TDomain> LoadOrDefaultAsync<TDomain>(this IRepository<TDomain> repository, Guid? id)
            where TDomain : class, IEntity<Guid?>, new()
        {
            return id.HasValue ? await repository.LoadAsync(id) : null;
        }

    }
}
