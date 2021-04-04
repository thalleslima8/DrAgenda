﻿using Newtonsoft.Json;

namespace DrAgenda.Shared.Dto
{
    public class ExceptionDetails
    {
        public int StatusCode { get; set; }
        
        public string Message { get; set; }

        public string Details { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
