namespace Padaria.Models.ViewModels
{
    public class ProdutoFormViewModel
    {
        public List<ProdutosConta> Produtos { get; set; }
        public Conta Conta { get; set; }
        public double Troco { get; set; }   



        public double ValorTotalProduto(int i)
        {
            return Produtos[i].Quantidade * Produtos[i].Produto.Preco ;
        }

        public double ValorTotal()
        {
            double valor = 0;
            for(int i = 0; i <Produtos.Count; i++)
            {
                valor += ValorTotalProduto(i);
            }
            return valor;
        }
    }
}
