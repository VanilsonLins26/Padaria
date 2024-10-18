using System.Net;

namespace Padaria.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public ICollection<ProdutosConta> Produtos { get; set; }
        public Encomenda ?Encomenda { get; set; }

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

        public double ValorTotal()
        {
            double soma = 0;
            foreach(ProdutosConta produto in Produtos)
            {
                soma += produto.Total();

            }
            return soma;
        }
    }
}
