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
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(List<ProdutosConta> p, Conta conta, string valorRecebido)
        {
            double vr = 0;
            if (valorRecebido != null)
            {
                vr = double.Parse(valorRecebido);
                 double.Parse(valorRecebido);
                if (conta.ValorTotal > 0 && vr == 0)
                {

                    _context.Add(conta);
                    foreach (var item in p)
                    {
                        item.Conta = conta;
                        _context.Add(item);

                    }
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            double troco = vr - conta.ValorTotal;
            ProdutoFormViewModel pf = new ProdutoFormViewModel { Produtos = p, Troco = troco };  
            return View(pf);
        }
    }
}
