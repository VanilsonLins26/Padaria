using System.Numerics;

namespace Padaria.Models
{
    public class ProdutosConta
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public Conta Conta { get; set; }

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
            return Quantidade * Produto.Preco;
        }
    }
}
