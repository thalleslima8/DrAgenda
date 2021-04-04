using System;

namespace DrAgenda.Api.Helpers
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
            
        }
    }
}
