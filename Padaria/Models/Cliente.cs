using System.ComponentModel.DataAnnotations;

namespace Padaria.Models
{
    public class Cliente
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, insira um nome")]
        public string Nome { get; set; }
        [Phone]
        [Required(ErrorMessage = "Por favor, insira um contato")]
        [StringLength(20, MinimumLength = 15, ErrorMessage = "Insira um numero valido")]
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
