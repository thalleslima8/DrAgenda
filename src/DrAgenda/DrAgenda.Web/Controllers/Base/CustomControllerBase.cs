using System.Linq;
using DrAgenda.Api.Client;
using DrAgenda.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NToastNotify;

namespace DrAgenda.Web.Controllers.Base
{
    public class CustomControllerBase : Controller
    {
        public CustomControllerBase(IWebHostEnvironment webHostEnvironment,
            DrAgendaService apiClient,
            AppUserState appUserState,
            IToastNotification toastNotification)
        {
            ApiClient = apiClient;
            AppUserState = appUserState;
            ToastNotification = toastNotification;
            WebHostEnvironment = webHostEnvironment;

            apiClient.UserId = AppUserState.UserId;
        }

        public IToastNotification ToastNotification { get; }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public DrAgendaService ApiClient { get; }

        public AppUserState AppUserState { get; }

        public void ShowErrorMessage(string message)
        {
            ToastNotification.AddErrorToastMessage(message, new ToastrOptions { Title = "Erro" });
        }

        public void ShowSuccessMessage(string message)
        {
            ToastNotification.AddSuccessToastMessage(message, new ToastrOptions { Title = "Sucesso" });
        }

        public void ShowAlertMessage(string message)
        {
            ToastNotification.AddAlertToastMessage(message, new ToastrOptions { Title = "Atenção" });
        }

        public void ShowInfoMessage(string message)
        {
            ToastNotification.AddInfoToastMessage(message, new ToastrOptions { Title = "Informação" });
        }

        public void ShowModelStateErroMessage(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;

                ShowErrorMessage(erroMsg);
            }
        }
    }
}