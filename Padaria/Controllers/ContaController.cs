using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;

namespace Padaria.Controllers
{
    public class ContaController : Controller
    {
        private readonly PadariaContext _context;

        public ContaController(PadariaContext context)
        {
            
            _context = context;
        }

        public IActionResult Index(List<Produto> ?p, string codigo)
        {
            var produto = _context.Produto.FirstOrDefault(p => p.Id.Equals(codigo));
            if (produto == null)
            {
                return NotFound();  
            }

            if (p == null)
            {
                List<Produto> produtosT = new List<Produto>(); 
                produtosT.Add(produto);
                return View(produtosT);
            }

            p.Add(produto);
            return View(p);
        }
    }
}
