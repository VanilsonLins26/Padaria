using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Padaria.Models;
using Padaria.Models.ViewModels;
using Padaria.Services;

namespace Padaria.Controllers
{
    public class EncomendasController : Controller
    {

        public readonly PadariaContext _context;
        public readonly ProdutoContaService _produtoContaService;   

        public EncomendasController(PadariaContext context, ProdutoContaService produtoContaService)
        {
            _context = context;
            _produtoContaService = produtoContaService;
        }

        public IActionResult Index()
        {
            var encomendas = _context.Encomenda.ToList();
            return View(encomendas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Encomenda encomenda)
        {
            _context.Add(encomenda);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult AddProduto(string codigo, EncomendaViewModel? encomenda)
        {
            
            var produto = _context.Produto.FirstOrDefault(p => p.Codigo.Equals(codigo));
            
            
                foreach (var item in encomenda.ProdutosConta)
                {

                    var p = _context.Produto.FirstOrDefault(p => p.Id == item.ProdutoId);
                    item.Produto = p;
                }
            
                if (produto != null)
                {

                    var pc = new ProdutoConta { Produto = produto, Quantidade = 1, ProdutoId = produto.Id };
                    pc.Total = _produtoContaService.ValorTotalProduto(pc);
                    encomenda.ProdutosConta.Add(pc);


                }
                ViewBag.Total = encomenda.ProdutosConta.Sum(p => p.Total);
            
            
            return View(nameof(Create), encomenda);
            
        }


    }
}
