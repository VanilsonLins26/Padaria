using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Padaria.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public ICollection<ProdutosConta> Produtos  { get; set; } = new List<ProdutosConta>();  
        public Encomenda? Encomenda { get; set; }

        [Display(Name ="Valor Total")]
        [DataType(DataType.Currency)]
        public double ValorTotal { get; set; }

        public Conta() { }

        public Conta(DateTime data)
        {
            Data = data;
        }
        public Conta(int id, DateTime data)
        {
            Data = data;
            Id = id;


        }

    }
}
