using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;
using Padaria.Models.ViewModels;

namespace Padaria.Controllers
{
    public class ContasController : Controller
    {
        private readonly PadariaContext _context;

        public ContasController(PadariaContext context)
        {
            
            _context = context;
        }

        public IActionResult Index()
        {
           var contas = _context.Conta.OrderByDescending(c => c.Data).ToList();
            return View(contas);
        }



       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<ProdutoConta> p, Conta conta)
        {
            
                
                    
                    foreach (var item in p)
                    {
                        var produto = _context.Produto.FirstOrDefault(pr => pr.Id == item.Produto.Id);

                        if (produto != null)
                        {
                            var pc = new ProdutoConta { Produto = produto, Quantidade = item.Quantidade, Conta = conta, Total = item.Total};
                            conta.Produtos.Add(pc);
                            _context.Add(conta);
                        }
                        
                        

                    }
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                
 
        }

        public IActionResult UnicSearch(DateTime data)
        {
            var contas = _context.Conta.Where(c => c.Data.Date == data).OrderByDescending(c => c.Data).ToList();
            ViewBag.Data = data;

            return View("Index", contas);

        }

        public IActionResult RangeSearch(DateTime dataInicial, DateTime dataFinal)
        {
            var contas = _context.Conta.Where(c => c.Data.Date >= dataInicial && c.Data.Date <= dataFinal).OrderByDescending(c => c.Data).ToList();
            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;  

            return View("Index", contas);

        }

        public IActionResult Details(int? id)
        {
           var conta = _context.Conta.Include(c => c.Produtos).ThenInclude(c => c.Produto).FirstOrDefault(c => c.Id == id);

            return View(conta);
        }
    }

   
}
