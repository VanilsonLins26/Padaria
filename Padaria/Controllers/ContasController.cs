using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;
using Padaria.Models.Enums;
using Padaria.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var encomendas = _context.Conta.Where(e => (e as Encomenda).Status == Status.Andamento);

            var contas = _context.Conta.Except(encomendas).OrderByDescending(c => c.Data).ToList();
            return View(contas);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<ProdutoConta> p, Conta conta)
        {

            var produtoConta = new ProdutoConta();

            foreach (var item in p)
            {
                produtoConta = _context.ProdutosConta.FirstOrDefault(p => p.ProdutoId == item.ProdutoId && p.Quantidade == item.Quantidade);
                var produto = _context.Produto.FirstOrDefault(pr => pr.Id == item.Produto.Id);
                
                    if (produtoConta == null)
                    {

                        produtoConta = new ProdutoConta { Produto = produto, Quantidade = item.Quantidade, Total = item.Total };


                    }

                    produto.QntDisponiveis -= item.Quantidade;
                    produto.QntVendidas += item.Quantidade;
                    if (produto.QntDisponiveis < 0)
                    {
                        produto.QntDisponiveis = 0;
                    }

                conta.Produtos.Add(produtoConta);

            }

            
            _context.Add(conta);

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");


        }

       
        

        public IActionResult Search(DateTime dataInicial, DateTime? dataFinal)
        {
            List<Conta> contas = new List<Conta>();
            if (dataFinal != null)
            {
                contas = _context.Conta.Where(c => c.Data.Date >= dataInicial && c.Data.Date <= dataFinal).OrderByDescending(c => c.Data).ToList();
               
            }
            else
            {
                contas = _context.Conta.Where(c => c.Data.Date == dataInicial).OrderByDescending(c => c.Data).ToList();
                

            }
            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;

            return View("Index", contas);

        }

        public IActionResult Details(int? id)
        {
            var conta = _context.Conta.Include(c => c.Produtos).ThenInclude(c => c.Produto).FirstOrDefault(c => c.Id == id);

            return View(conta);
        }

        public IActionResult Report(List<int> id)
        {
            
            List<ProdutoConta> produtos = new List<ProdutoConta>();
            List<Conta> conta= new List<Conta>();
            
            foreach(var item in id)
            {
                var c = _context.Conta.Include(c => c.Produtos).ThenInclude(c => c.Produto).FirstOrDefault(c => c.Id == item);
                conta.Add(c);   
                foreach (var i in c.Produtos)
                {
                    produtos.Add(i);
                }
            }
            var contagem = produtos.GroupBy(c => c.Produto.Nome).Select(g => new RelatorioContaFormaView{ Nome = g.Key, Quantidade = g.Sum(q => q.Quantidade) }).OrderByDescending(q => q.Quantidade).ToList();
            ViewBag.Total = conta.Sum(c => c.ValorTotal);
            ViewBag.Vendas = conta.Count();
            ViewBag.Media = ViewBag.Total/ViewBag.Vendas;
             
              

            return View(contagem);
        }
    }


}
