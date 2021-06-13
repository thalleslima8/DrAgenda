using System;
using System.Diagnostics;
using System.Linq;
using DrAgenda.Api.Client;
using DrAgenda.Shared.Dto.Base;
using DrAgenda.Shared.Dto.ControleAcesso;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DrAgenda.Web.Helpers.AccessControl
{
    public class LoggingActionFilter : Attribute, IActionFilter
    {
        private readonly DrAgendaService _drAgendaService;
        private readonly AppUserState _appUserState;

        public LoggingActionFilter(DrAgendaService drAgendaService,
            AppUserState appUserState)
        {
            _drAgendaService = drAgendaService;
            _appUserState = appUserState;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!Debugger.IsAttached)
            {
                if (_appUserState.IsAuthenticated)
                {
                    var bodyRequest = string.Empty;

                    if (context.ActionArguments.ContainsKey("model"))
                    {
                        var modelArgument = context.ActionArguments["model"];

                        if (modelArgument != null)
                        {
                            bodyRequest = JsonConvert.SerializeObject(modelArgument);
                        }
                    }

                    var newLog = new LogAcessoDto
                    {
                        Usuario = new DtoAggregate {Id = _appUserState.UserId},
                        DataHoraAcesso = DateTime.Now,
                        HostLocalAcesso = context.HttpContext.Connection.LocalIpAddress.ToString(),
                        HostPublicoAcesso = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                        MethodRequest = context.HttpContext.Request.Method,
                        CaminhoUrl = context.HttpContext.Request.Path.Value,
                        UserAgent = context.HttpContext.Request.Headers["User-Agent"].FirstOrDefault(),
                        BodyRequest = bodyRequest
                    };

                    var result = _drAgendaService.LogAcessoApi.Post(newLog).Result;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}