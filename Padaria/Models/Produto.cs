using Padaria.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Padaria.Models
{
    public class Produto
    {
        public int Id { get; set; }
       
        public string Codigo { get; set; }
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString ="{0:F2}")]
        [DataType(DataType.Currency)]
        public double Preco { get; set; }
        public Tipo Tipo { get; set; }
        public ICollection<ProdutoConta>? Produtos { get; set; }

        public Produto() { }

        public Produto(int id, string codigo, string nome, double preco, Tipo tipo)
        {
            Codigo = codigo;
            Nome = nome;
            Preco = preco;
            Tipo = tipo;
            Id = id;
            
        }


    }
}
