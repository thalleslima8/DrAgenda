using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrAgenda.Web.Models.Home
{
    public class AutenticacaoDuplaViewModel
    {
        [DisplayName("Código")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public int? Codigo { get; set; }
    }
}
