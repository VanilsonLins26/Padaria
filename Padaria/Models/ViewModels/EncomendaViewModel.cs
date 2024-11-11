using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Padaria.Models.ViewModels
{
    public class EncomendaViewModel
    {
        
        
        
        public Encomenda Encomenda  { get; set; }
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        
    
    }
}
