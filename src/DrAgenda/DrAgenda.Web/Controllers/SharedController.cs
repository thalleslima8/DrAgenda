using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DrAgenda.Api.Client;
using DrAgenda.Web.Controllers.Base;
using DrAgenda.Web.Helpers;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace DrAgenda.Web.Controllers
{
    public class SharedController : CustomControllerBase
    {
        public SharedController(IWebHostEnvironment webHostEnvironment,
            DrAgendaService apiClient,
            AppUserState appUserState,
            IToastNotification toastNotification)
            : base(webHostEnvironment, apiClient, appUserState, toastNotification)
        {
        }

        [HttpPost]
        public virtual ActionResult KendoExportSave(string contentType, string base64, string fileName)
        {
            return File(Convert.FromBase64String(base64), contentType, fileName);
        }
    }
        
}