using Microsoft.AspNetCore.Mvc;
using Padaria.Models;
using System.Diagnostics;
using Padaria.Models.ViewModels;
using Padaria.Models.Enums;
using Padaria.Services;
using Microsoft.Extensions.Caching.Memory;


namespace Padaria.Controllers
{
    public class VendasController : Controller
    {
        private readonly PadariaContext _context;
        private readonly IMemoryCache _cache;
        private readonly ProdutoContaService _produtoContaService;
        private readonly ContaService _contaService;
        private readonly ProdutoService _produtoService;

        public VendasController(ContaService contaService, ProdutoContaService produtoContaService, ProdutoService produtoService, IMemoryCache cache, PadariaContext context)
        {

            _contaService = contaService;
            _produtoContaService = produtoContaService;
            _produtoService = produtoService;
            _cache = cache;
            _context = context;
        }

        public IActionResult Index()
        {
            
            if (!_context.Produto.Any())
            {
                var P1 = new Produto { Id = 1, Codigo = "1", Nome = "Pão", QntDisponiveis = 1111111, QntVendidas = 0, Tipo = Tipo.Unidade, ValorUnitario = 0.5 };
                var P2 = new Produto { Id = 2, Codigo = "2", Nome = "Pão de côco(P)", QntDisponiveis = 1111111, QntVendidas = 0, Tipo = Tipo.Unidade, ValorUnitario = 1.0 };
                var P3 = new Produto { Id = 3, Codigo = "3", Nome = "Pão de côco(G)", QntDisponiveis = 1111111, QntVendidas = 0, Tipo = Tipo.Unidade, ValorUnitario = 6.0 };
                var P4 = new Produto { Id = 4, Codigo = "4", Nome = "Margarina", QntDisponiveis = 1111111, QntVendidas = 0, Tipo = Tipo.Unidade, ValorUnitario = 4.5 };
                var P5 = new Produto { Id = 5, Codigo = "5", Nome = "Leite caixa", QntDisponiveis = 1111111, QntVendidas = 0, Tipo = Tipo.Unidade, ValorUnitario = 7.0 };
                var P6 = new Produto { Id = 6, Codigo = "6", Nome = "Bolo recheado", QntDisponiveis = 1111111, QntVendidas = 0, Tipo = Tipo.Unidade, ValorUnitario = 4.0 };
                _context.AddRange(P1, P2, P3, P4, P5, P6);
                _context.SaveChanges();
            }


            return View();
        }

        public IActionResult Create(Conta conta)
        {
            return View(conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            var conta = new Conta {Data =DateTime.Now , Status = StatusConta.NaoFinalizado };
            await _contaService.AddAsync(conta);
             _cache.Set("Conta", conta, TimeSpan.FromMinutes(20));
            
            return View(conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduto(string codigo)
        {

            var c = _cache.Get<Conta>("Conta");

            var produto = await _produtoService.FindByCodeAsync(codigo);
            if(produto == null)
            {
                TempData["Error"] = "Produto não encontrado";
                return View(nameof(Create), c);
                
            }
            
            var produtoConta = c.Produtos.FirstOrDefault(pc => pc.ProdutoId == produto.Id);
            
            if (produtoConta != null)
            {
                produtoConta.Quantidade += 1;

                _produtoService.Detache(produto);
                await _produtoContaService.UpdateAsync(produtoConta);
                return View(nameof(Create), c);
            }
            produtoConta = new ProdutoConta {Conta = c, Produto = produto, Quantidade = 1, ValorUnitario = produto.ValorUnitario };
            c.Produtos.Add(produtoConta);
            await _contaService.UpdateAsync(c);
            _cache.Set("Conta", c, TimeSpan.FromMinutes(20));
            return View(nameof(Create), c);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeQuantity(ProdutoConta produtoConta)
        {

            var conta = _cache.Get<Conta>("Conta");
            var produtoC = conta.Produtos.FirstOrDefault(pc => pc.Id == produtoConta.Id);   
             
            if (produtoConta.Quantidade == 0)
            {
                await _produtoContaService.RemoveAsync(produtoC);
                conta.Produtos.Remove(produtoC);
            }
            else
            {
                produtoC.Quantidade = produtoConta.Quantidade;
                await _produtoContaService.UpdateAsync(produtoConta);
            }
            
            
            return View(nameof(Create), conta);
        }

        public async Task<IActionResult> Finish(Conta conta)
        {
            var c = _cache.Get<Conta>("Conta");
            if(c == null)
            {
                return RedirectToAction(nameof(Index));
            }
            foreach (var item in c.Produtos)
            {
                item.Produto.QntDisponiveis -= item.Quantidade;
                item.Produto.QntVendidas += item.Quantidade;
                await _produtoService.UpdateAsync(item.Produto);
            }
            c.Status = StatusConta.Concluido; 
            c.Data = conta.Data;
            c.MetodoPagamento = conta.MetodoPagamento;
            await _contaService.UpdateAsync(c);
            return RedirectToAction(nameof(Index)); 

        }


       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
