using System;
using DrAgenda.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using DrAgenda.Api.Client;
using DrAgenda.Shared.Dto.ControleAcesso;
using DrAgenda.Web.Controllers.Base;
using DrAgenda.Web.Helpers;
using DrAgenda.Web.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DrAgenda.Web.Controllers
{
    public class HomeController : CustomControllerBase
    {
        private Random _random = new Random();
        private const string _chaveAutenticacao = "AUTENTICACAO.KEY";

        public HomeController(IWebHostEnvironment webHostEnvironment,
            DrAgendaService apiClient,
            AppUserState appUserState,
            IToastNotification toastNotification)
            : base(webHostEnvironment, apiClient, appUserState, toastNotification) { }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> Login(string returnUrl)
        {
            if (AppUserState.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index");
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                UsuarioDto usuario;

                try
                {
                    usuario = await ApiClient.UsuarioApi.Login(new LoginDto
                    {
                        UserName = model.Username,
                        Password = model.Password
                    });
                }
                catch (HttpRequestException exception)
                {
                    ModelState.AddModelError("Password", "Nome de usuário ou senha inválidos");
                    return View(model);
                }

                if (usuario != null)
                {
                    if (usuario.AutenticacaoDoisFatores)
                    {
                        const string accountSid = "AC1b0334ddce506bce2bf51c1ef5b2e20f";
                        const string authToken = "845e3d2be0bd2e83bb68646f182b2f85";

                        TwilioClient.Init(accountSid, authToken);

                        var codigo = _random.Next(0, 9999);

                        var telefone = usuario.Telefone
                            .Replace("(", "")
                            .Replace(")", "")
                            .Replace("-", "");

                        if (string.IsNullOrWhiteSpace(telefone))
                            return RedirectToAction("Index");

                        var message = MessageResource.Create(
                            body: $"Seu código para autenticação no sistema CODTRAN é {codigo}",
                            from: new Twilio.Types.PhoneNumber("+14847026060"),
                            to: new Twilio.Types.PhoneNumber($"+55{telefone}")
                        );

                        HttpContext.Session.Set(_chaveAutenticacao, new AutenticacaoViewModel
                        {
                            Codigo = codigo,
                            Usuario = usuario,
                            KeepConnected = model.KeepConnected,
                            ReturnUrl = model.ReturnUrl
                        });

                        return RedirectToAction("AutenticacaoDupla");
                    }

                    await AppUserState.Login(usuario, model.KeepConnected);

                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);

                    return RedirectToAction("Index");

                }

                ModelState.AddModelError("Password", "Nome de usuário ou senha inválidos");

                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Password", e.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> AutenticacaoDupla()
        {
            var model = new AutenticacaoDuplaViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AutenticacaoDupla(AutenticacaoDuplaViewModel model)
        {
            var autenticacao = HttpContext.Session.Get<AutenticacaoViewModel>(_chaveAutenticacao);

            if (autenticacao == null)
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            if (autenticacao.Codigo != model.Codigo)
            {
                ModelState.AddModelError("Codigo", "Código inválido");
                return View(model);
            }

            await AppUserState.Login(autenticacao.Usuario, autenticacao.KeepConnected);

            if (!string.IsNullOrWhiteSpace(autenticacao.ReturnUrl))
                return Redirect(autenticacao.ReturnUrl);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await AppUserState.Logout();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> NaoAutorizado()
        {
            return View();
        }
    }
}
