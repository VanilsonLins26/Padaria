using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;
using Padaria.Models.ViewModels;

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
            var c = new ClientFormView { Clientes = clientes };
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
            return RedirectToAction(nameof(Create), "Encomendas");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cliente cliente)
        {
            if (ModelState.IsValid)
            {



                _context.Update(cliente);
                _context.SaveChanges();
                

            }
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Details(int id)
        {

            var cliente = _context.Cliente.Include(c => c.Encomendas).FirstOrDefault(c => c.Id == id);
            cliente.Encomendas = cliente.Encomendas.OrderByDescending(e => e.Data).ToList();
            return View(cliente);

        }

        public IActionResult Search(string nome)
        {
            if(nome == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var clientes = _context.Cliente.Where(c => c.Nome.Contains(nome)).ToList();
            var c = new ClientFormView { Clientes = clientes };
            return View(nameof(Index) ,c);  

        }
    }
}
