using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;
using Padaria.Models.Enums;
using Padaria.Models.ViewModels;
using Padaria.Services;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Padaria.Controllers
{
    public class ContasController : Controller
    {
        private readonly ContaService _contaService;
        private readonly ProdutoContaService _produtoContaService;
        private readonly ProdutoService _produtoService;

        public ContasController(ContaService contaService, ProdutoContaService produtoContaService, ProdutoService produtoService)
        {
            _produtoContaService = produtoContaService; 
            _contaService = contaService;
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Index()
        {
            

            var contas = await _contaService.FindAllAsync();
            return View(contas);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<ProdutoConta> p, Conta conta)
        {

            var produtoConta = new ProdutoConta();

            foreach (var item in p)
            {
                produtoConta = await _produtoContaService.FindAsync(item);
                var produto = await _produtoService.FindByIdAsync(item.ProdutoId);
                
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

            
            await _contaService.AddConta(conta);

            return RedirectToAction("Index", "Home");


        }

       
        

        public async Task<IActionResult> Search(DateTime dataInicial, DateTime? dataFinal)
        {
            
            
           var contas = await _contaService.SearchByDateAsync(dataInicial, dataFinal); 

            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;

            return View("Index", contas);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "id não recebido" });
            }
            
                var conta = await _contaService.FindByIdAsync(id.Value);
            if (conta == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(conta);
        }

        public async Task<IActionResult> Report(List<int> id)
        {
            
            List<ProdutoConta> produtos = new List<ProdutoConta>();
            List<Conta> conta= new List<Conta>();
            
            foreach(var item in id)
            {
                var c = await _contaService.FindByIdAsync(item);
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

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };
            return View(viewModel);
        }
    }


}
