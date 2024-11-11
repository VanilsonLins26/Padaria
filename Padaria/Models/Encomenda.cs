using Microsoft.AspNetCore.Mvc;
using Padaria.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Padaria.Models
{
    public class Encomenda : Conta
    {  

        public DateTime DataPedido { get; set; }

       
        public Cliente? Cliente { get; set; }
        [ForeignKey(nameof(Cliente))]
        [Required(ErrorMessage = "Por favor, selecione um cliente.")]
        public int? ClienteId { get; set; }

        [Display(Name ="Observações")]
        public string? Obs { get; set; }
        public override double ValorTotal { get { return Produtos.Sum(p => p.Total) - Desconto; } }
        [DataType(DataType.Currency)]
        public double ValorAntecipado { get; set; }
        [DataType(DataType.Currency)]
        public double Desconto { get; set; }


        public Encomenda() { }

        public Encomenda(int id, DateTime data, DateTime dataPedido, Cliente cliente, StatusConta status, double valorTotal,MetodoPagamento metodoPagamento, string obs, double valorAntecipado, double desconto ) : base(id, data, metodoPagamento, status )
        {
             
            DataPedido = dataPedido;
            Cliente = cliente;
            Desconto = desconto;
            Obs = obs;
            ValorAntecipado = valorAntecipado;  

           
        }

        
        
    }
}
