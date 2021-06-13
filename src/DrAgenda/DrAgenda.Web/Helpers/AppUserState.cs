using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DrAgenda.Shared.Dto.ControleAcesso;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace DrAgenda.Web.Helpers
{
    public class AppUserState
    {
        private readonly HttpContext _context;

        public AppUserState(IHttpContextAccessor context)
        {
            _context = context.HttpContext;
        }

        public AppUserState(HttpContext context)
        {
            _context = context;
        }

        public bool IsAuthenticated => _context.User.Identity.IsAuthenticated;

        public Guid? UserId
        {
            get
            {
                if (Guid.TryParse(Get(ClaimTypes.NameIdentifier), out var id))
                    return id;
                return null;
            }
        }

        public string UserName => Get(ClaimTypes.Name);

        public bool IsInRole(params string[] roles)
        {
            var claimRole = Get(ClaimTypes.Role);

            if (!string.IsNullOrWhiteSpace(claimRole))
            {
                var accessPermissions = claimRole.Split(";");
                return roles.Any(x => accessPermissions.Contains(x));
            }

            return false;
        }

        public async Task Login(UsuarioDto usuario, bool isPersistent)
        {
            var roles = usuario.PerfisAcesso.SelectMany(x => x.PermissoesAcesso).Select(x => x.Acao).ToList();

            if (usuario.Admin)
                roles.Add("Admin");

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new (ClaimTypes.Name, usuario.Nome),
                new (ClaimTypes.Email, usuario.Email),
                new (ClaimTypes.Role, string.Join(";", roles)),
            };

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaims(claims);

            await _context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddHours(16),
                    IsPersistent = isPersistent,
                    AllowRefresh = true,
                });
        }

        public async Task Logout()
        {
            await _context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private string Get(string key)
        {
            var user = _context.User;

            if (user == null)
                return string.Empty;

            var claim = user.Claims.ToList().FirstOrDefault(c => c.Type == key);

            return claim != null ? claim.Value : string.Empty;
        }
    }
}
