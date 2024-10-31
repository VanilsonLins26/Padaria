namespace Padaria.Models.ViewModels
{
    public class EncomendaViewModel
    {
        public List<ProdutoConta> ProdutosConta { get; set; } = new List<ProdutoConta>();   
        public Encomenda Encomenda  { get; set; }
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public Cliente Cliente { get; set; }
    }
}
