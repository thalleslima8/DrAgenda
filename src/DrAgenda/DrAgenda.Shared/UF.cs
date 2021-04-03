using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrAgenda.Shared
{
    public enum UF
    {
        [Display(Name = "Acre")]
        [Description("Acre")]
        AC,
        [Display(Name = "Alagoas")]
        [Description("Alagoas")]
        AL,
        [Description("Amapá")]
        [Display(Name = "Amapá")]
        AP,
        [Description("Amazonas")]
        [Display(Name = "Amazonas")]
        AM,
        [Description("Bahia")]
        [Display(Name = "Bahia")]
        BA,
        [Description("Ceará")]
        [Display(Name = "Ceará")]
        CE,
        [Description("Distrito Federal")]
        [Display(Name = "Distrito Federal")]
        DF,
        [Description("Espirito Santo")]
        [Display(Name = "Espirito Santo")]
        ES,
        [Description("Goiás")]
        [Display(Name = "Goiás")]
        GO,
        [Description("Maranhão")]
        [Display(Name = "Maranhão")]
        MA,
        [Description("Mato Grosso")]
        [Display(Name = "Mato Grosso")]
        MT,
        [Description("Mato Grosso do Sul")]
        [Display(Name = "Mato Grosso do Sul")]
        MS,
        [Description("Minas Gerais")]
        [Display(Name = "Minas Gerais")]
        MG,
        [Description("Pará")]
        [Display(Name = "Pará")]
        PA,
        [Description("Paraiba")]
        [Display(Name = "Paraiba")]
        PB,
        [Description("Paraná")]
        [Display(Name = "Paraná")]
        PR,
        [Description("Pernambuco")]
        [Display(Name = "Pernambuco")]
        PE,
        [Description("Piauí")]
        [Display(Name = "Piauí")]
        PI,
        [Description("Rio de Janeiro")]
        [Display(Name = "Rio de Janeiro")]
        RJ,
        [Description("Rio Grande do Norte")]
        [Display(Name = "Rio Grande do Norte")]
        RN,
        [Description("Rio Grande do Sul")]
        [Display(Name = "Rio Grande do Sul")]
        RS,
        [Description("Rondônia")]
        [Display(Name = "Rondônia")]
        RO,
        [Description("Roraima")]
        [Display(Name = "Roraima")]
        RR,
        [Description("Santa Catarina")]
        [Display(Name = "Santa Catarina")]
        SC,
        [Description("São Paulo")]
        [Display(Name = "São Paulo")]
        SP,
        [Description("Sergipe")]
        [Display(Name = "Sergipe")]
        SE,
        [Description("Tocantins")]
        [Display(Name = "Tocantins")]
        TO
    }
}
