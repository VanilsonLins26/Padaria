namespace Padaria.Models.ViewModels
{
    public class ProdutoFormViewModel
    {
        public List<ProdutosConta> Produtos { get; set; }
        public Conta Conta { get; set; }
        public double Troco { get; set; }   



        public double ValorTotal()
        {
            double valor = 0;
            foreach (var item in Produtos)
            {
                valor += item.Total();
            }
            return valor;
        }
    }
}
