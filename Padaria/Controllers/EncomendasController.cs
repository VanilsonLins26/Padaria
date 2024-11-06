﻿using Microsoft.AspNetCore.Mvc;
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

        public readonly EncomendaService _encomendaService;
        public readonly ProdutoContaService _produtoContaService;
        public readonly ClienteService _clienteService;
        public readonly ProdutoService _produtoService;
        public EncomendasController(EncomendaService encomendaService, ProdutoContaService produtoContaService, ClienteService clienteService, ProdutoService produtoService)
        {
            _encomendaService =encomendaService;
            _produtoContaService = produtoContaService;
            _clienteService = clienteService;
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Index()
        {

            var agrupamento = await _encomendaService.GroupFindAsync();
            return View(agrupamento);
        }

        public async Task<IActionResult> Create()
        {
            var clientes = await _clienteService.FindAllAsync();
            var e = new EncomendaViewModel { Clientes = clientes };

           
            return View(e);

            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Encomenda encomenda)
        {
            if (ModelState.IsValid)
            {
                var item = encomenda.Produtos;
                for (int i = 0; i < item.Count; i++)
                {
                    var produto = await _produtoService.FindByIdAsync(item[i].ProdutoId);
                    var pc = await _produtoContaService.FindAsync(item[i]);

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


                var cliente = await _clienteService.FindByIdAsync(encomenda.ClientId);
                encomenda.Cliente = cliente;
               await _encomendaService.AddAsync(encomenda);
                return RedirectToAction(nameof(Index));
            }
            var c = await _clienteService.FindAllAsync();
            var e = new EncomendaViewModel {Encomenda = encomenda, Clientes = c };
            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                // Aqui você pode logar ou simplesmente exibir o erro
                Console.WriteLine(modelError.ErrorMessage);
                
            }

            return View(e);

        }

        public async Task<IActionResult> AddProduto(string? codigo, EncomendaViewModel? encomenda)
        {

            if (codigo != null)
            {


                var produto = await _produtoService.FindByCodeAsync(codigo);

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

                var p = await _produtoService.FindByIdAsync(item.ProdutoId);
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


                var cliente = await _clienteService.FindByIdAsync(encomenda.Cliente.Id);
            if (cliente == null)
            {
                return NotFound();
            }
            encomenda.Clientes.Add(cliente);  
           
            var total = encomenda.ProdutosConta.Sum(p => p.Total);
            ViewBag.Total = total;
            




            return View(nameof(Create), encomenda);

        }

        public async Task<IActionResult> SearchCLiente(string cliente)
        {
            var clientes = await _clienteService.FindByNameAsync(cliente);
            if (cliente.IsNullOrEmpty())
            {
                return BadRequest();
            }
            var e = new EncomendaViewModel { Clientes = clientes };

            return View(nameof(Create), e);
        }

        public async Task<IActionResult> Details(int id)
        {
            var encomenda = await _encomendaService.FindById(id);
            return View(encomenda);

        }

        public async Task<IActionResult> ConfirmarEntrega(int id, MetodoPagamento metodoPagamento)
        {
            var enc = await _encomendaService.FindById(id);   
            enc.Status = Status.Concluido;
            enc.MetodoPagamento = metodoPagamento;
            enc.Data = DateTime.Now;    
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


    }
}
