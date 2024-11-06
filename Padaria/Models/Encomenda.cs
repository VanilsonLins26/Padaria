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
       
        public int ClientId { get; set; }
        public Status Status { get; set; }
        [Display(Name ="Observações")]
        public string? Obs { get; set; }
        public double? ValorAntecipado { get; set; }


        public Encomenda() { }

        public Encomenda(int id, DateTime data, DateTime dataPedido, Cliente cliente, Status status, double valorTotal,MetodoPagamento metodoPagamento, string obs, double valorAntecipado ) : base(id, data, metodoPagamento, valorTotal )
        {
             
            DataPedido = dataPedido;
            Cliente = cliente;
            Status = status;
            Obs = obs;
            ValorAntecipado = valorAntecipado;  

           
        }

        
        
    }
}
