using System.ComponentModel.DataAnnotations;

namespace Padaria.Models.Enums
{
    public enum Status : int
    {
        [Display(Name ="Pedido Feito")]
        Andamento = 0,
        Concluido = 1,
        


    }
}
