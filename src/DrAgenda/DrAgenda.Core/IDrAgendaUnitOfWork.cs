using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codout.Framework.DAL;
using DrAgenda.Core.IRepository;

namespace DrAgenda.Core
{
    public interface IDrAgendaUnitOfWork : IUnitOfWork
    {
        ICarteiraRepository Carteria { get; }
        IConsultaRepository Consulta { get; }
        IEnderecoRepository Endereco { get; }
        IMovimentoRepository Movimento { get; }
        IPacienteRepository Paciente { get; }
        IProfissionalRepository Profissional { get; }
        IProntuarioRepository Prontuario { get; }
        IAcessoBloqueadoPeriodoRepository AcessoBloqueadoPeriodo { get; } 
        IHorarioAcessoRepository HorarioAcesso { get; } 
        ILogAcessoRepository LogAcesso { get; } 
        IPerfilAcessoRepository PerfilAcesso { get; } 
        IPermissaoAcessoRepository PermissaoAcesso { get; } 
        IUsuarioRepository Usuario { get; } 
    }
}
