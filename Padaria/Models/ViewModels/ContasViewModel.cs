namespace Padaria.Models.ViewModels
{
    public class ContasViewModel
    {
        public Conta Conta {  get; set; }
        public ICollection<ProdutosConta> Produtos { get; set; }
    }
}
