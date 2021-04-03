using System.Collections.Generic;
using System.Collections.ObjectModel;

using DrAgenda.Core.Dominio.Base;

namespace DrAgenda.Core.Dominio.ControleAcesso
{
    public class PerfilAcesso : EntityAudit
    {
        public virtual string Nome { get; set; }

        #region Permissões de Acesso
        private readonly IList<PermissaoAcesso> _permissoesAcesso = new List<PermissaoAcesso>();

        public virtual IReadOnlyCollection<PermissaoAcesso> PermissoesAcesso => new ReadOnlyCollection<PermissaoAcesso>(_permissoesAcesso);

        public virtual void AdicionarPermissaoAcesso(PermissaoAcesso item)
        {
            if (!_permissoesAcesso.Contains(item))
                _permissoesAcesso.Add(item);
        }

        public virtual void RemoverPermissoesAcesso()
        {
            _permissoesAcesso.Clear();
        }
        #endregion Permissoes
    }
}
