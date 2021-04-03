using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Codout.Framework.Common.Security;
using DrAgenda.Core.Dominio.Base;

namespace DrAgenda.Core.Dominio.ControleAcesso
{
    public class Usuario : EntityAudit
    {
        private string _senha;
        private DateTime? _dataExpiracaoToken;
        private Guid? _token;

        private readonly ISet<PerfilAcesso> _perfilAcessos = new HashSet<PerfilAcesso>();
        private readonly ISet<HorarioAcesso> _horariosAcesso = new HashSet<HorarioAcesso>();
        private readonly ISet<AcessoBloqueadoPeriodo> _acessosBloqueadoPeriodos = new HashSet<AcessoBloqueadoPeriodo>();

        public virtual string Nome { get; set; }
        public virtual string NomeUsuario { get; set; }
        public virtual string Email { get; set; }
        public virtual bool Inativo { get; set; }
        public virtual bool Admin { get; set; }
        public virtual string Senha => _senha;
        public virtual DateTime? DataExpiracaoToken => _dataExpiracaoToken;
        public virtual Guid? Token => _token;
        public virtual bool AutenticacaoDoisFatores { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string CodigoAgente { get; set; }

        public virtual ReadOnlyCollection<PerfilAcesso> PerfilAcessos => new ReadOnlyCollection<PerfilAcesso>(_perfilAcessos.ToList());
        public virtual ReadOnlyCollection<HorarioAcesso> HorariosAcesso => new ReadOnlyCollection<HorarioAcesso>(_horariosAcesso.ToList());
        public virtual ReadOnlyCollection<AcessoBloqueadoPeriodo> AcessosBloqueadoPeriodos => new ReadOnlyCollection<AcessoBloqueadoPeriodo>(_acessosBloqueadoPeriodos.ToList());

        public virtual void AdicionarPerfilAcesso(PerfilAcesso item)
        {
            if (!_perfilAcessos.Contains(item))
                _perfilAcessos.Add(item);
        }

        public virtual void LimparPerfisAcesso()
        {
            _perfilAcessos.Clear();
        }

        public virtual void AdicionarHorarioAcesso(HorarioAcesso item)
        {
            if (!_horariosAcesso.Contains(item))
                _horariosAcesso.Add(item);
        }

        public virtual void LimparHorariosAcesso()
        {
            _horariosAcesso.Clear();
        }

        public virtual void AdicionarAcessoBloqueadoPeriodo(AcessoBloqueadoPeriodo item)
        {
            if (!_acessosBloqueadoPeriodos.Contains(item))
                _acessosBloqueadoPeriodos.Add(item);
        }

        public virtual void LimparAcessoBloqueadoPeriodo()
        {
            _perfilAcessos.Clear();
        }

        public virtual void DefinirSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(NomeUsuario))
                throw new Exception("Informe o nome de usuário antes de informar a senha");

            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("Informe a senha");

            _senha = GetMd5Hash(senha, Encoding.UTF8.GetBytes(NomeUsuario));
        }

        public virtual bool Autenticar(string password)
        {
            return VerifyMd5Hash(password, _senha);
        }

        private static string GetMd5Hash(string input, byte[] saltBytes)
        {
            return SimpleHash.ComputeHash(input, HashAlgorithmType.Md5, saltBytes);
        }

        private static bool VerifyMd5Hash(string input, string hash)
        {
            return SimpleHash.VerifyHash(input, HashAlgorithmType.Md5, hash);
        }

        public virtual void GerarToken()
        {
            _token = Guid.NewGuid();
            _dataExpiracaoToken = DateTime.Today.AddDays(1);
        }

        public virtual bool ResetarSenha(string novaSenha, Guid token)
        {
            var tokenValido = VerificarValidadeToken(token);

            if (!tokenValido)
                return false;

            DefinirSenha(novaSenha);

            _token = null;
            _dataExpiracaoToken = null;

            return true;
        }

        public virtual bool VerificarValidadeToken(Guid token)
        {
            if (!_token.HasValue
                || _token.Value != token)
                return false;

            if (_dataExpiracaoToken.HasValue
                && _dataExpiracaoToken.Value.Date < DateTime.Today)
                return false;

            return true;
        }
    }
}