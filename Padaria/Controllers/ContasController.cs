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
           var contas = _context.Conta.ToList();
            return View(contas);
        }

        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<ProdutosConta> p, Conta conta, string valorRecebido)
        {
            double vr = 0;
            if (valorRecebido != null)
            {
                vr = double.Parse(valorRecebido);
                 double.Parse(valorRecebido);
                if (conta.ValorTotal > 0 && vr == conta.ValorTotal)
                {

                    
                    foreach (var item in p)
                    {
                        var produto = _context.Produto.FirstOrDefault(pr => pr.Id == item.Produto.Id);

                        if (produto != null)
                        {
                            var pc = new ProdutosConta { Produto = produto, Quantidade = item.Quantidade, Conta = conta, Total = item.Total};
                            conta.Produtos.Add(pc);
                            _context.Add(conta);
                        }
                        
                        

                    }
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            double troco = vr - conta.ValorTotal;
            ProdutoFormViewModel pf = new ProdutoFormViewModel { Produtos = p, Troco = troco };  
            return View(pf);
        }
        public IActionResult Details(int? id)
        {
           var conta = _context.Conta.Include(c => c.Produtos).ThenInclude(c => c.Produto).FirstOrDefault(c => c.Id == id);

            return View(conta);
        }
    }

   
}
