using System.Collections.Generic;
using System.Collections.ObjectModel;
using DrAgenda.Core.Dominio.Base;

namespace Codtran.Core.Domain.ControleAcesso
{
    public class PermissaoAcesso : Entity
    {
        private readonly IList<PermissaoAcesso> _permissoesAcesso = new List<PermissaoAcesso>();

        public virtual string Nome { get; set; }

        public virtual string Acao { get; set; }

        public virtual PermissaoAcesso PermissaoAcessoPai { get; set; }

        public virtual IReadOnlyCollection<PermissaoAcesso> PermissoesAcesso => new ReadOnlyCollection<PermissaoAcesso>(_permissoesAcesso);
    }
}
