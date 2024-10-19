using System.Numerics;

namespace Padaria.Models
{
    public class ProdutosConta
    {
        public int Id { get; set; }
        public int? ContaId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public virtual Conta Conta { get; set; }
        

        public ProdutosConta() { }

        public ProdutosConta(int id, Produto produto, int quantidade, Conta conta)
        {
            Produto = produto;
            Quantidade = quantidade;
            Conta = conta;
            Id = id;
        }

        public double Total()
        {
            double t =Quantidade * Produto.Preco;
            return t;
        }
    }
}
