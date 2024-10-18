using System.ComponentModel.DataAnnotations;

namespace Padaria.Models.Enums
{
    public enum Status : int
    {
        [Display(Name ="Pedido Feito")]
        Andamento = 0,
        Pago = 1,
        Entregue = 2,
        Cancelado = 3,


    }
}
