using Microsoft.AspNetCore.Mvc;
using Padaria.Models;
using System.Diagnostics;
using Padaria.Models.ViewModels;
using Padaria.Models.Enums;
using Padaria.Services;

namespace Padaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProdutoContaService _produtoContaService;
        private readonly PadariaContext _context;

        public HomeController( PadariaContext context, ProdutoContaService produtoContaService)
        {
            
            _context = context;
            _produtoContaService = produtoContaService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(List<ProdutoConta>? p, string? codigo)
        {

            if (!ModelState.IsValid)
            {
                
                return View();
            }

            if (codigo != null)
            {


                var produto = await _produtoContaService.FindByCodeAsync(codigo);
                if (produto == null)
                {
                    return NotFound();
                }



                ProdutoConta pc = new ProdutoConta { Produto = produto, Quantidade = 1, Total = produto.Preco };

                p.Add(pc);




            }

            foreach (var item in p)
                {
                
                if (item.Quantidade == 0)
                    {
                        p.Remove(item);

                        break;
                    }
                var valor = _produtoContaService.ValorTotalProduto(item);
                item.Total = valor;
            }

                var mp = new List<MetodoPagamento>();

                ProdutoFormViewModel pf = new ProdutoFormViewModel { Produtos = p };

            var valorTotal = _produtoContaService.ValorTotal(p);
            ViewBag.Total = valorTotal; 


                return View(pf);

            
            
        }

        

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
