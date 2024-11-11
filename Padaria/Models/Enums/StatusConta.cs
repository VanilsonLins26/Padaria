using System.ComponentModel.DataAnnotations;

namespace Padaria.Models.Enums
{
    public enum StatusConta : int
    {
        NaoFinalizado = 0,
        [Display(Name = "Em andamento")]
        Andamento = 1,
        Concluido = 2
    }
}
