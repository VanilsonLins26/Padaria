using Padaria.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Padaria.Models
{
    public class Produto
    {


        public int Id { get; set; }
        [Required(ErrorMessage ="Insira um código")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Insira um nome")]
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString ="{0:F2}")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Insira o preço")]
        public double ValorUnitario { get; set; }
        [Required(ErrorMessage = "Insira o tipo")]
        public Tipo Tipo { get; set; }
        public int QntVendidas { get; set; }    
        public int? QntDisponiveis { get; set; }
        public ICollection<ProdutoConta>? Produtos { get; set; }

        public Produto() { }

        public Produto(int id, string codigo, string nome, double valorUnitario, Tipo tipo, int qntVendidas, int qntDisponiveis)
        {
            Codigo = codigo;
            Nome = nome;
            ValorUnitario = valorUnitario;
            Tipo = tipo;
            Id = id;
            QntDisponiveis = qntDisponiveis;
            QntVendidas = qntVendidas;  

            
        }


    }
}
