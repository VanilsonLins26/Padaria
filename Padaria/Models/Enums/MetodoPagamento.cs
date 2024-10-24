using System.ComponentModel.DataAnnotations;

namespace Padaria.Models.Enums
{
    public enum MetodoPagamento :int
    {
        Dinheiro = 0,
        Pix = 1,
        [Display(Name = "Débito")]
        Debito = 2,
        [Display(Name = "Crédito")]
        Credito = 3,
    }
}
