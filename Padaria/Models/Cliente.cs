using System.ComponentModel.DataAnnotations;

namespace Padaria.Models
{
    public class Cliente
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        [Phone]
        public string Contato { get; set; }
        public ICollection<Encomenda> ?Encomendas { get; set; }


        public Cliente() { }

        public Cliente(int id,string nome, string contato)
        {
            Nome = nome;
            Contato = contato;
            Id = id;
            
        }
    }
}
