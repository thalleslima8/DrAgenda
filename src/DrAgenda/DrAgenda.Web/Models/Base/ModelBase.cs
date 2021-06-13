using System;
using System.Threading.Tasks;
using DrAgenda.Api.Client;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DrAgenda.Web.Models.Base
{
    public abstract class ModelBase : IModel
    {
        public Guid? Id { get; set; }

        public virtual async Task Bind(DrAgendaService apiClient)
        {
          
        }

        public virtual bool IsValid(DrAgendaService apiClient, ModelStateDictionary modelState)
        {
            return modelState.IsValid;
        }
    }
}