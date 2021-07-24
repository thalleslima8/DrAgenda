using System;
using System.Linq;
using System.Threading.Tasks;
using Codout.Framework.Api.Dto.Default;
using Codout.Kendo.DynamicLinq;
using DrAgenda.Api.Client;
using DrAgenda.Api.Client.Apis.Base;
using DrAgenda.Web.Helpers;
using DrAgenda.Web.Helpers.AccessControl;
using DrAgenda.Web.Models.Base;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using DataSourceRequest = Kendo.Mvc.UI.DataSourceRequest;

namespace DrAgenda.Web.Controllers.Base
{
    public abstract class CrudJsonController<TDto, TViewModel, TWebApi> : CustomControllerBase
        where TWebApi : DrAgendaApiClient<TDto>
        where TDto : DtoBase<Guid?>, new()
        where TViewModel : class, IModel, new()
    {
        protected CrudJsonController(IWebHostEnvironment webHostEnvironment,
            DrAgendaService apiClient,
            AppUserState appUserState,
            IToastNotification toastNotification) : base(webHostEnvironment, apiClient, appUserState, toastNotification) { }

        public abstract string Title { get; }

        public abstract Task<TViewModel> ToModel(TDto dto);

        public abstract Task<TDto> ToDto(TViewModel model);

        private void SetViewBagTitle()
        {
            ViewBag.Title = Title;
        }

        #region Novo
       
        [HttpPost]
        [ControleAcesso("Novo")]
        public virtual async Task<ActionResult> New(TViewModel model)
        {
            SetViewBagTitle();
            if (model.IsValid(ApiClient, ModelState))
            {
                var dto = ToDto(model);

                if (dto != null)
                {
                    await ApiClient.GetService<TWebApi>().Post(await dto);

                    return Json(new {ok = true, mensagem = "Cadastro salvo com sucesso!" });
                }
            }

            await model.Bind(ApiClient);
            return Json(new { ok = false, mensagem = "Não foi possivel realizar o cadastro" });
        }


        #endregion

        #region Editar
        
        [HttpPost]
        [ControleAcesso("Editar")]
        public virtual async Task<ActionResult> Edit(TViewModel model)
        {
            SetViewBagTitle();
            if (model.IsValid(ApiClient, ModelState))
            {
                var domain = await ToDto(model);

                if (domain != null)
                {
                    await ApiClient.GetService<TWebApi>().Put(domain);
                    return Json(new { ok = true, mensagem = "Cadastro atualizado com sucesso!" });
                }
            }

            await model.Bind(ApiClient);
            return Json(new { ok = false, mensagem = "Não foi possivel realizar o cadastro" });
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
            var sort = request.Sorts?.Select(x =>
                    new Sort
                    {
                        Dir = x.SortDirection == ListSortDirection.Ascending ? "Asc" : "Desc",
                        Field = x.Member
                    })
                .ToList();

            Filter filter = null;

            if (request.Filters != null)
                foreach (var filterDescriptor in request.Filters)
                {
                    if (filterDescriptor is CompositeFilterDescriptor)
                    {
                        var composite = (CompositeFilterDescriptor)filterDescriptor;
                        filter = new Filter
                        {
                            Logic = composite.LogicalOperator.ToString(),
                            Filters = composite.FilterDescriptors.Select(x => new Filter
                            {
                                Logic = "And",
                                Field = ((FilterDescriptor)x).Member,
                                Operator = ((FilterDescriptor)x).Operator.GetLogic(),
                                Value = ((FilterDescriptor)x).Value
                            })
                        };
                    }
                    else
                    {
                        filter = new Filter
                        {
                            Logic = "And",
                            Filters = request.Filters.Select(x => new Filter
                            {
                                Logic = "And",
                                Field = ((FilterDescriptor)x).Member,
                                Operator = ((FilterDescriptor)x).Operator.GetLogic(),
                                Value = ((FilterDescriptor)x).Value
                            })
                        };
                    }
                }

            var result = await ApiClient.GetService<TWebApi>().DataSource(request.PageSize, ((request.Page <= 0 ? 1 : request.Page) - 1) * request.PageSize, sort, filter, null);
            return result;
        }

        #endregion
    }
}