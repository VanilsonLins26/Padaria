using Microsoft.AspNetCore.Mvc;
using Padaria.Models;
using System.Diagnostics;
using Padaria.Models.ViewModels;

namespace Padaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PadariaContext _context;

        public HomeController(ILogger<HomeController> logger, PadariaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Produto> produtos = null;
            return View(produtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<Produto> p, string codigo)
        {
            Produto produto = _context.Produto.FirstOrDefault(p => p.Codigo.Equals(codigo));
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
