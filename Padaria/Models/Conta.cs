using Padaria.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Padaria.Models
{
    public class Conta
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Insira uma data")]
        public DateTime Data { get; set; }
        public ICollection<ProdutoConta> Produtos  { get; set; } = new List<ProdutoConta>();  
        [Display(Name ="Valor Total")]
        [DataType(DataType.Currency)]
        public virtual double ValorTotal { get { return Produtos.Sum(p => p.Total); } }
        public StatusConta Status { get; set; }
        public MetodoPagamento? MetodoPagamento { get; set; }

        public Conta() { }

       
        public Conta(int id, DateTime data, MetodoPagamento metodoPagamento, StatusConta statusConta)
        {
            Data = data;
            Id = id;
            MetodoPagamento = metodoPagamento;
            Status = statusConta;
              

        }

    }
}
