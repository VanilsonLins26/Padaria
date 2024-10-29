using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Padaria.Models
{
    public class ProdutoConta
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public ICollection<Conta> Contas { get; set; } = new List<Conta>();
        [DataType(DataType.Currency)]
        [Display(Name="Valor")]
        public double Total { get; set; }


        public ProdutoConta() { }

        public ProdutoConta(int id, Produto produto, int quantidade, double total)
        {
            Produto = produto;
            Quantidade = quantidade;
            Id = id;
            Total = total;
        }
    }

       
}
