using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Padaria.Models;
using Padaria.Models.Enums;
using Padaria.Models.ViewModels;
using Padaria.Services;
using System.Linq;
using System.Reflection;

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
            
            var agrupamento = _context.Encomenda.Where(e=> e.Status == Status.Andamento).Include(e => e.Cliente).OrderBy(e => e.Data).GroupBy(e => e.Data.Date).ToList();
            return View(agrupamento);
        }

        public IActionResult Create()
        {
            var clientes = _context.Cliente.OrderBy(c => c.Nome).ToList();
            var e = new EncomendaViewModel { Clientes = clientes };

           
            return View(e);

            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Encomenda encomenda)
        {
            if (!ModelState.IsValid)
            {
                var item = encomenda.Produtos;
                for (int i = 0; i < item.Count; i++)
                {
                    var produto = _context.Produto.FirstOrDefault(p => p.Id == item[i].ProdutoId);
                    var pc = _context.ProdutosConta.FirstOrDefault(pc => pc.Quantidade == item[i].Quantidade && pc.Produto == produto);

                    produto.QntDisponiveis -= item[i].Quantidade;
                    produto.QntVendidas += item[i].Quantidade;
                    if (produto.QntDisponiveis < 0)
                    {
                        produto.QntDisponiveis = 0;
                    }
                    if (pc != null)
                    {
                        item[i] = pc;
                    }



                }


                var cliente = _context.Cliente.Where(c => c.Id == encomenda.Cliente.Id).FirstOrDefault();
                encomenda.Cliente = cliente;
                _context.Add(encomenda);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(encomenda);

        }

        public IActionResult AddProduto(string? codigo, EncomendaViewModel? encomenda)
        {

            if (codigo != null)
            {


                var produto = _context.Produto.FirstOrDefault(p => p.Codigo.Equals(codigo));

                if (produto == null)
                {
                    TempData["ErrorMessage"] = "Produto não encontrado";

                }



                else
                {

                    var pc = new ProdutoConta { Produto = produto, Quantidade = 1, ProdutoId = produto.Id };

                    encomenda.ProdutosConta.Add(pc);


                }
            }
            
            foreach (var item in encomenda.ProdutosConta)
            {

                var p = _context.Produto.FirstOrDefault(p => p.Id == item.ProdutoId);
                item.Produto = p;
                item.Total = _produtoContaService.ValorTotalProduto(item);
                
                

            }
            foreach (var item in encomenda.ProdutosConta)
            {
                if (item.Quantidade == 0)
                {
                    encomenda.ProdutosConta.Remove(item);
                    break;
                }
            }


                var cliente = _context.Cliente.FirstOrDefault(c => c.Id == encomenda.Cliente.Id);
            if (cliente == null)
            {
                return NotFound();
            }
            encomenda.Clientes.Add(cliente);    
            var total = encomenda.ProdutosConta.Sum(p => p.Total);
            ViewBag.Total = total;
            




            return View(nameof(Create), encomenda);

        }

        public IActionResult SearchCLiente(string cliente)
        {
            var clientes = _context.Cliente.Where(c => c.Nome.Contains(cliente)).OrderBy(c => c.Nome).ToList();
            if (cliente.IsNullOrEmpty())
            {
                return BadRequest();
            }
            var e = new EncomendaViewModel { Clientes = clientes };

            return View(nameof(Create), e);
        }

        public IActionResult Details(int id)
        {
            var encomenda = _context.Encomenda.Include(e => e.Cliente).Include(e => e.Produtos).ThenInclude(p => p.Produto).FirstOrDefault(e => e.Id == id);
            return View(encomenda);

        }

        public IActionResult ConfirmarEntrega(int id, MetodoPagamento metodoPagamento)
        {
            var enc = _context.Encomenda.FirstOrDefault(e => e.Id == id);    
            enc.Status = Status.Concluido;
            enc.MetodoPagamento = metodoPagamento;
            enc.Data = DateTime.Now;    
            _context.Update(enc);
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Completed()
        {
            var concluidos = _context.Encomenda.Where(e=> e.Status == Status.Concluido).Include(e => e.Cliente).OrderByDescending(e => e.Data).ToList();    
            return View(concluidos);


        }

        public IActionResult Search(DateTime dataInicial, DateTime? dataFinal)
        {
            List<Encomenda> concluidos = new List<Encomenda>();
            if (dataFinal != null)
            {
                concluidos = _context.Encomenda.Where(e => e.Data.Date >= dataInicial && e.Data.Date <= dataFinal && e.Status == Status.Concluido).OrderByDescending(e => e.Data).ToList();

            }
            else
            {
                concluidos = _context.Encomenda.Where(e => e.Data.Date == dataInicial && e.Status == Status.Concluido).OrderByDescending(e => e.Data).ToList();


            }
            ViewBag.DataInicial = dataInicial;
            ViewBag.DataFinal = dataFinal;

            return View("Completed", concluidos);

        }


    }
}
