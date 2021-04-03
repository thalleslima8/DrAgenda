using Codout.Framework.DAL;
using Codout.Framework.NH;
using Codtran.Core;
using DrAgenda.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DrAgenda.Data.Helpers
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ITenant>(x => new DefaultTenant("Codtran.Data", "DefaultTenant", connectionString));

            services.AddSingleton<IUnitOfWorkFactory<IDrAgendaUnitOfWork>, UnitOfWorkFactory>();

            services.AddScoped<IDrAgendaUnitOfWork, DrAgendaUnitOfWork>();

            return services;
        }
    }
}