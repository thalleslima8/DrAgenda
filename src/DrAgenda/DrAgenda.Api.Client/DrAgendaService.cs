
using System;
using System.Collections.Generic;
using DrAgenda.Api.Client;

namespace Codtran.Api.Client
{
    public class DrAgendaService
    {
        private readonly object _lock = new object();

        private readonly IDictionary<Type, object> _services = new Dictionary<Type, object>();

        public string ApiEndpoint { get; }

        private string ApiKey { get; }

        public DrAgendaService(ApiSettings settings)
        {
            ApiKey = settings.ApiKey;
            ApiEndpoint = settings.ApiEndpoint;
        }

        public T GetService<T>()
        {
            var serviceType = typeof(T);

            lock (_lock)
            {
                if (!_services.ContainsKey(serviceType))
                {
                    var service = Activator.CreateInstance(serviceType, ApiEndpoint, ApiKey);
                    _services.Add(serviceType, service);
                }
            }

            return (T)_services[serviceType];
        }

        //public PerfilAcessoApi PerfilAcessoApi => GetService<PerfilAcessoApi>();
        //public PermissaoAcessoApi PermissaoAcessoApi => GetService<PermissaoAcessoApi>();
        
    }
}
