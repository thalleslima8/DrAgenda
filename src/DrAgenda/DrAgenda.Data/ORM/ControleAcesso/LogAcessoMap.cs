using DrAgenda.Core.Dominio.ControleAcesso;
using DrAgenda.Data.ORM.Base;

namespace DrAgenda.Data.ORM.ControleAcesso
{
    public class LogAcessoMap : AuditClassMapBase<LogAcesso>
    {
        public LogAcessoMap() : base("TBLogsAcessos")
        {
            Not.LazyLoad();

            Map(x => x.DataHoraAcesso).Nullable();

            Map(x => x.CaminhoUrl).Nullable();

            Map(x => x.HostPublicoAcesso).Nullable();

            Map(x => x.HostLocalAcesso).Nullable();

            Map(x => x.UserAgent).Nullable();

            Map(x => x.MethodRequest).Nullable();

            Map(x => x.BodyRequest).Nullable();
            
            References(x => x.Usuario);
        }
    }
}