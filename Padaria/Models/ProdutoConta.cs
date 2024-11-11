using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Padaria.Models
{
    public class ProdutoConta
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ContaId { get; set; }
        public Produto Produto { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Valor unitário")]
        public double ValorUnitario { get; set; }
        public Conta Conta { get; set; }
        public int Quantidade { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public double Total => Quantidade*ValorUnitario;


        public ProdutoConta() { }

        public ProdutoConta(int id, Produto produto, int quantidade, Conta conta, double valorUnitario)
        {
            Produto = produto;
            Quantidade = quantidade;
            Id = id;
            Conta = conta;  
            ValorUnitario = valorUnitario;  
            
        }
    }

       
}
