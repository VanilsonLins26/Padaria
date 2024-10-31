using Microsoft.AspNetCore.Mvc;
using Padaria.Models;

namespace Padaria.Controllers
{
    public class ClientesController : Controller

       
    {

        private readonly PadariaContext _context;

        public ClientesController(PadariaContext context)
        {

            _context = context;

        }

        public IActionResult Index()
        {
            var clientes = _context.Cliente.ToList();   
            return View(clientes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction(nameof(Create), "Encomendas");

        }
    }
}
