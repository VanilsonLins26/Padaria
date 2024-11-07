using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;
using Padaria.Models.Enums;
using Padaria.Models.ViewModels;
using Padaria.Services;
using Padaria.Services.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;



namespace Padaria.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly PadariaContext _context;
        private readonly ProdutoService _produtoService;

        public ProdutosController(PadariaContext context, ProdutoService produtoService)
        {
            _context = context;
            _produtoService = produtoService;
        }





        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.FindAllAsync();
            return View(produtos);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtoService.FindByIdAsync(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                var existe = await _produtoService.AnyAsync(produto);
                if (existe)
                {
                    ModelState.AddModelError("Codigo", "Já existe um produto com este código");
                    return View(produto);
                }
                await _produtoService.AddAsync(produto);
                return RedirectToAction(nameof(Index));

            }
            return View(produto);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtoService.FindByIdAsync(id.Value);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto, int quantidade)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                var existe = await _produtoService.AnyAsync(produto);
                if (existe)
                {
                    ModelState.AddModelError("Codigo", "Já existe um produto com este código");
                    return View(produto);
                }
                try
                {
                    produto.QntDisponiveis += quantidade;
                    await _produtoService.UpdateAsync(produto);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _produtoService.FindByIdAsync(id.Value);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            try
            {
                await _produtoService.RemoveAsync(id);

            }

            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }




            return RedirectToAction(nameof(Index));


        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Search(string nome)
        {
            List<Produto> produtos = new List<Produto>();
            if (string.IsNullOrEmpty(nome))
                produtos = await _produtoService.FindAllAsync();

            else
                produtos = await _produtoService.SearchAsync(nome);

            return View(nameof(Index), produtos);

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
