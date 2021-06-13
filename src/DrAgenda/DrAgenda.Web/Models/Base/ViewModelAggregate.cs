using System;

namespace DrAgenda.Web.Models.Base
{
    public class ViewModelAggregate : ModelBase
    {
        public ViewModelAggregate()
        {
        }

        public ViewModelAggregate(Guid? id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public string Descricao { get; set; }
    }
}