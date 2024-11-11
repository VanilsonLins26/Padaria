using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

using Padaria.Models;
using Padaria.Models.Enums;
using Padaria.Models.ViewModels;
using Padaria.Services;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Padaria.Controllers
{
    public class EncomendasController : Controller
    {
        private readonly PadariaContext _context;
        public readonly IMemoryCache _cache;
        public readonly EncomendaService _encomendaService;
        public readonly ProdutoContaService _produtoContaService;
        public readonly ClienteService _clienteService;
        public readonly ProdutoService _produtoService;
        public EncomendasController(PadariaContext context, EncomendaService encomendaService, ProdutoContaService produtoContaService, ClienteService clienteService, ProdutoService produtoService, IMemoryCache cache)
        {
            _context = context;
            _encomendaService =encomendaService;
            _produtoContaService = produtoContaService;
            _clienteService = clienteService;
            _produtoService = produtoService;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {

            var agrupamento = await _encomendaService.GroupFindAsync();
            return View(agrupamento);
        }

        public async Task<IActionResult> Create(int? id)
        {
            Encomenda encomenda = new Encomenda();
            if (id == null)
            {
                 encomenda = _cache.Get<Encomenda>("encomenda");
            }
            else
            {
                encomenda = await _encomendaService.FindById(id.Value);
                _cache.Set("encomenda", encomenda, TimeSpan.FromMinutes(20));
            }
            var clientes = await _clienteService.FindAllAsync();
            var ev = new EncomendaViewModel { Encomenda = encomenda, Clientes = clientes };
            return View(ev);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Encomenda encomenda)
        {
            
            
            await _encomendaService.AddAsync(encomenda);  
            var e = new EncomendaViewModel {Encomenda = encomenda};
            _cache.Set("encomenda", encomenda, TimeSpan.FromMinutes(20));

           
            return View(e);

            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateClient(Cliente cliente)
        {
            if(cliente.Contato == null){
                return BadRequest();
            }
            await _clienteService.InsertAsync(cliente);
            return RedirectToAction(nameof(Create));

        }

        

        public async Task<IActionResult> AddProduto(string? codigo)
        {
            var encomenda = _cache.Get<Encomenda>("encomenda");
            if (codigo != null)
            {


                var produto = await _produtoService.FindByCodeAsync(codigo);

                if (produto == null)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado";

                }
                else
                {
                    var produtoConta = encomenda.Produtos.FirstOrDefault(pc => pc.ProdutoId == produto.Id);

                    if (produtoConta != null)
                    {
                        produtoConta.Quantidade += 1;

                        _produtoService.Detache(produto);

                    }
                    else
                    {
                        produtoConta = new ProdutoConta { Conta = encomenda, Produto = produto, Quantidade = 1, ValorUnitario = produto.ValorUnitario };
                        encomenda.Produtos.Add(produtoConta);
                    }
                }
               
               

            }
            await _encomendaService.UpdateAsync(encomenda);
            _cache.Set("encomenda", encomenda, TimeSpan.FromMinutes(20));
            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> ChangeQuantity(ProdutoConta produtoConta)
        {

            var encomenda = _cache.Get<Conta>("encomenda");
            var produtoC = encomenda.Produtos.FirstOrDefault(pc => pc.Id == produtoConta.Id);

            if (produtoConta.Quantidade == 0)
            {
                await _produtoContaService.RemoveAsync(produtoC);
                encomenda.Produtos.Remove(produtoC);
            }
            else
            {
                produtoC.Quantidade = produtoConta.Quantidade;
                await _produtoContaService.UpdateAsync(produtoConta);
            }


            return RedirectToAction(nameof(Create));



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finish(Encomenda encomenda)
        {
            var e = _cache.Get<Encomenda>("encomenda");
            if (!ModelState.IsValid)
            {
                var enc = new EncomendaViewModel { Encomenda = e };
                return View(nameof(Create), enc);
            }
            
            e.Data = encomenda.Data;
            e.Status = StatusConta.Andamento;
           
            e.ClienteId = encomenda.ClienteId;
            e.ValorAntecipado = encomenda.ValorAntecipado;
            e.Obs = encomenda.Obs;
            e.Desconto = encomenda.Desconto;    
            await _encomendaService.UpdateAsync(e);
            return RedirectToAction(nameof(Index));
        }

       

        public async Task<IActionResult> Details(int id)
        {
            var encomenda = await _encomendaService.FindById(id);
            return View(encomenda);

        }

        public async Task<IActionResult> ConfirmarEntrega(int id, MetodoPagamento metodoPagamento)
        {
            var enc = await _encomendaService.FindById(id);   
            enc.Status = StatusConta.Concluido;
            enc.MetodoPagamento = metodoPagamento;
            enc.Data = DateTime.Now;
            foreach (var item in enc.Produtos)
            {
                item.Produto.QntDisponiveis -= item.Quantidade;
                item.Produto.QntVendidas += item.Quantidade;
                await _produtoService.UpdateAsync(item.Produto);
            }
            await _encomendaService.UpdateAsync(enc);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Completed()
        {
            var concluidos = await _encomendaService.FindAllCompleted();
            return View(concluidos);


        }

        public async Task<IActionResult> Search(DateTime dataInicial, DateTime? dataFinal)
        {
            var concluidos = await _encomendaService.FindCompletedByDate(dataInicial, dataFinal); 
            
            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;

            return View("Completed", concluidos);

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var encomenda = await _encomendaService.FindById(id);
            

            return View(encomenda);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Encomenda encomenda)
        { 
            if (encomenda == null)
            {
                return NotFound();
            }

            await _encomendaService.UpdateAsync(encomenda);

            return RedirectToAction(nameof(Index));
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            
            await _encomendaService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult BuscarPessoas(string term)
        {
            if(term == null)
            {
                var p = _context.Cliente
                .Select(p => new { id = p.Id, text = p.Nome })
                .ToList();

               return Json(p);
            }
            var pessoas = _context.Cliente
                .Where(p => p.Nome.Contains(term))  // Filtra o nome conforme o texto digitado
                .Select(p => new { id = p.Id, text = p.Nome })
                .ToList();

            return Json(pessoas);  // Retorna o JSON diretamente
        }

    }
}
