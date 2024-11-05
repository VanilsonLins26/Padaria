using System.ComponentModel.DataAnnotations;

namespace Padaria.Models.Enums
{
    public enum Status : int
    {
        [Display(Name ="Em andamento")]
        Andamento = 0,
        Concluido = 1,
        


    }
}
