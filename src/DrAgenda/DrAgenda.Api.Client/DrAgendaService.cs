using System;
using System.Collections.Generic;
using DrAgenda.Api.Client.Apis;
using DrAgenda.Api.Client.Apis.ControleAcesso;
using DrAgenda.Api.Client.Apis.Financeiro;
using DrAgenda.Api.Client.Apis.Operacional;
using DrAgenda.Api.Client.Apis.Person;

namespace DrAgenda.Api.Client
{
    public class DrAgendaService
    {
        private readonly object _lock = new object();

        private readonly IDictionary<Type, object> _services = new Dictionary<Type, object>();

        public string ApiEndpoint { get; }

        private string ApiKey { get; }

        public Guid? UserId { get; set; }

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
                    object? service;

                    if(UserId.HasValue)
                        service = Activator.CreateInstance(serviceType, ApiEndpoint, ApiKey, UserId);
                    else
                        service = Activator.CreateInstance(serviceType, ApiEndpoint, ApiKey);

                    _services.Add(serviceType, service);
                }
            }

            return (T)_services[serviceType];
        }

        public PerfilAcessoApi PerfilAcessoApi => GetService<PerfilAcessoApi>();
        public PermissaoAcessoApi PermissaoAcessoApi => GetService<PermissaoAcessoApi>();
        public AcessoBloqueadoPeriodoApi AcessoBloqueadoPeriodoApi => GetService<AcessoBloqueadoPeriodoApi>();
        public HorarioAcessoApi HorarioAcessoApi => GetService<HorarioAcessoApi>();
        public LogAcessoApi LogAcessoApi => GetService<LogAcessoApi>();
        public UsuarioApi UsuarioApi => GetService<UsuarioApi>();
        public CarteiraApi CarteiraApi => GetService<CarteiraApi>();
        public MovimentoApi MovimentoApi => GetService<MovimentoApi>();
        public ConsultaApi ConsultaApi => GetService<ConsultaApi>();
        public ProntuarioApi ProntuarioApi => GetService<ProntuarioApi>();
        public EnderecoApi EnderecoApi => GetService<EnderecoApi>();
        public PacienteApi PacienteApi => GetService<PacienteApi>();
        public ProfissionalApi ProfissionalApi => GetService<ProfissionalApi>();
        public ConfiguracaoApi ConfiguracaoApi => GetService<ConfiguracaoApi>();
        
    }
}
