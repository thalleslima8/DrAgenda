using System;
using System.Collections.Generic;
using DrAgenda.Shared.Dto.Model;

namespace DrAgenda.Api.Client
{
    public class DrAgendaApiException : Exception
    {
        public IList<ErroDto> Errors { get; } = new List<ErroDto>();

        public DrAgendaApiException(string message, params ErroDto[] errors)
            : base(message)
        {
            if (errors == null) 
                return;

            foreach (var error in errors)
                Errors.Add(error);
        }
    }
}
