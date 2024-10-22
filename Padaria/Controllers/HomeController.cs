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
            List<ProdutosConta> produtos = null;
            return View(produtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<ProdutosConta> p, string? codigo)
        {
            if (codigo != null)
            {
                Produto produto = _context.Produto.FirstOrDefault(p => p.Codigo.Equals(codigo));
                if (produto == null)
                {
                    return NotFound();
                }

                ProdutosConta pc = new ProdutosConta { Produto = produto, Quantidade = 1, Total = produto.Preco };
                p.Add(pc);
                 
            }
            foreach (var item in p)
            {
                if (item.Quantidade == 0)
                {
                    p.Remove(item);
               
                    break;
                }
            }

                ProdutoFormViewModel pf = new ProdutoFormViewModel { Produtos = p };

                return View(pf);

            
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Finalizar(){

            return View();
            
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
