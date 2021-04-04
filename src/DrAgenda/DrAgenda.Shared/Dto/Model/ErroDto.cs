using System;
using System.ComponentModel;
using DrAgenda.Shared.Enums;

namespace DrAgenda.Shared.Dto.Model
{
    public class ErroDto
    {
        public ErroDto(ErrorCode errorCode, params string[] args)
        {
            Codigo = ((int)errorCode).ToString();
            Descricao = args != null ? string.Format(GetDescription(errorCode), args) : null;
        }

        private static string GetDescription(Enum value)
        {
            if (value == null)
                return string.Empty;

            var fieldInfo = value.GetType()?.GetField(value.ToString());

            if (fieldInfo == null)
                return value.ToString();
            
            return Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute customAttribute 
                ? customAttribute.Description 
                : value.ToString();
        }

        public string Codigo { get; }

        public string Descricao { get; }
    }
}
