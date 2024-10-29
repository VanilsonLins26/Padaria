namespace Padaria.Models.ViewModels
{
    public class EncomendaViewModel
    {
        public List<ProdutoConta> ProdutosConta { get; set; } = new List<ProdutoConta>();   
        public Encomenda Encomenda  { get; set; }
    }
}
