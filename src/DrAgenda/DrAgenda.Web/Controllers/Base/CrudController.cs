using System;
using System.Threading.Tasks;
using Codout.Framework.Api.Dto.Default;
using DrAgenda.Api.Client;
using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Web.Helpers;
using DrAgenda.Web.Helpers.AccessControl;
using DrAgenda.Web.Models.Base;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using DataSourceRequest = Kendo.Mvc.UI.DataSourceRequest;

namespace DrAgenda.Web.Controllers.Base
{
    public abstract class CrudController<TDto, TViewModel, TWebApi> : CustomControllerBase
        where TWebApi : DrAgendaApiClient<TDto>
        where TDto : DtoBase<Guid?>, new()
        where TViewModel : class, IModel, new()
    {
        protected CrudController(IWebHostEnvironment webHostEnvironment,
            DrAgendaService apiClient,
            AppUserState appUserState,
            IToastNotification toastNotification) : base(webHostEnvironment, apiClient, appUserState, toastNotification)
        {
        }

        public abstract string Title { get; }

        public abstract Task<TViewModel> ToModel(TDto dto);

        public abstract Task<TDto> ToDto(TViewModel model);

        protected void SetViewBagTitle()
        {
            ViewBag.Title = Title;
        }

        #region Index

        [ControleAcesso("Consultar")]
        public virtual ActionResult Index()
        {
            try
            {
                SetViewBagTitle();
                return View("_List");
            }
            catch (Exception e)
            {
                ShowErrorMessage($"Não foi possivel concluir a solicitação: {e.Message}");
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        #region Novo

        [ControleAcesso("Novo")]
        public virtual async Task<ActionResult> New()
        {
            var model = new TViewModel();
            try
            {
                SetViewBagTitle();
                await model.Bind(ApiClient);
                return View("_EditOrCreate", model);
            }
            catch (Exception e)
            {
                await model.Bind(ApiClient);
                ShowErrorMessage($"Não foi possivel concluir a solicitação: {e.Message}");
                return View("_EditOrCreate", model);
            }
        }

        [HttpPost]
        [ControleAcesso("Novo")]
        public virtual async Task<ActionResult> New(TViewModel model)
        {
            try
            {
                SetViewBagTitle();

                if (!model.IsValid(ApiClient, ModelState))
                {
                    ShowModelStateErroMessage(ModelState);

                    await model.Bind(ApiClient);

                    return View("_EditOrCreate", model);
                }

                var dto = ToDto(model);

                if (dto != null)
                {
                    await ApiClient.GetService<TWebApi>().Post(await dto);

                    ShowSuccessMessage("Cadastro salvo com sucesso!");

                    return RedirectToAction("Index");
                }

                await model.Bind(ApiClient);

                return View("_EditOrCreate", model);
            }
            catch (Exception e)
            {
                await model.Bind(ApiClient);
                ShowErrorMessage($"Não foi possivel concluir a solicitação: {e.Message}");
                return View("_EditOrCreate", model);
            }
        }
        #endregion

        #region Editar

        [ControleAcesso("Editar")]
        public virtual async Task<ActionResult> Edit(Guid? id)
        {
            var model = new TViewModel();
            try
            {
                SetViewBagTitle();
                var domain = await ApiClient.GetService<TWebApi>().Get(id);
                model = await ToModel(domain);
                await model.Bind(ApiClient);
                return View("_EditOrCreate", model);
            }
            catch (Exception e)
            {
                await model.Bind(ApiClient);
                ShowErrorMessage($"Não foi possivel concluir a solicitação: {e.Message}");
                return View("_EditOrCreate", model);
            }
        }

        [HttpPost]
        [ControleAcesso("Editar")]
        public virtual async Task<ActionResult> Edit(TViewModel model)
        {
            try
            {
                SetViewBagTitle();

                if (!model.IsValid(ApiClient, ModelState))
                {
                    ShowModelStateErroMessage(ModelState);

                    await model.Bind(ApiClient);

                    return View("_EditOrCreate", model);
                }

                var domain = await ToDto(model);

                if (domain != null)
                {
                    await ApiClient.GetService<TWebApi>().Put(domain);

                    ShowSuccessMessage("Cadastro atualizado com sucesso!");

                    return RedirectToAction("Index");
                }

                await model.Bind(ApiClient);

                return View("_EditOrCreate", model);
            }
            catch (Exception e)
            {
                await model.Bind(ApiClient);
                ShowErrorMessage($"Não foi possivel concluir a solicitação: {e.Message}");
                return View("_EditOrCreate", model);
            }
        }

        #endregion

        #region Excluir
        [HttpPost]
        [ControleAcesso("Excluir")]
        public virtual async Task<JsonResult> Delete(Guid? id)
        {
            SetViewBagTitle();
            try
            {
                await ApiClient.GetService<TWebApi>().Delete(id);
                return Json(new { result = true });
            }
            catch (Exception e)
            {
                return Json(new { result = true, message = e.Message });
            }

        }
        #endregion

        #region DataHandler
        [ControleAcesso("Consultar", Action = "Index")]
        public virtual async Task<object> DataHandler([ModelBinder(BinderType = typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        {
            return await ApiClient.GetService<TWebApi>().DataSource(request.ToSourceRequestApi());
        }
        #endregion
    }
}
