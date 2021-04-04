using System;


namespace DrAgenda.Shared.Dto.Base
{
    public class DtoAggregate : DtoBase
    {
        public DtoAggregate()
        {
        }

        public DtoAggregate(Guid? id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public string Descricao { get; set; }
    }
}
