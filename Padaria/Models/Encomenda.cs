using Microsoft.AspNetCore.Mvc;
using Padaria.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Padaria.Models
{
    public class Encomenda : Conta
    {
        [ForeignKey("Conta")]
        
        public DateTime DataPedido { get; set; }
        public Cliente? Cliente { get; set; }
        public Status Status { get; set; }
        

        public Encomenda() { }

        public Encomenda(int id, DateTime data, DateTime dataPedido, Cliente cliente, Status status, double valorTotal,MetodoPagamento metodoPagamento ) : base(id, data, metodoPagamento, valorTotal )
        {
             
            DataPedido = dataPedido;
            Cliente = cliente;
            Status = status;

           
        }

        
        
    }
}
