using Microsoft.AspNetCore.Mvc;
using Padaria.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Padaria.Models
{
    public class Encomenda : Conta
    {
        [ForeignKey("Conta")]
        public  int ContaId { get; set; }
        public DateTime DataEntrega { get; set; }
        public Cliente Cliente { get; set; }
        public Status Status { get; set; }
        public Conta Conta { get; set; }

        public Encomenda() { }

        public Encomenda(DateTime data, DateTime dataEntrega, Cliente cliente) : base(data)
        {
            DataEntrega = dataEntrega;
            Cliente = cliente;
           
        }

        
        
    }
}
