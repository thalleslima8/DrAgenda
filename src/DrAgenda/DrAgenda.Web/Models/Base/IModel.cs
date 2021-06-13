using System;
using System.Threading.Tasks;
using DrAgenda.Api.Client;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DrAgenda.Web.Models.Base
{
    public interface IModel
    {
        Guid? Id { get; set; }

        Task Bind(DrAgendaService apiClient);

        bool IsValid(DrAgendaService apiClient, ModelStateDictionary modelState);
    }
}