using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Padaria.Models;
using Padaria.Models.ViewModels;
using Padaria.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Padaria.Controllers
{
    public class ClientesController : Controller

       
    {

        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {

            _clienteService = clienteService;

        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.FindAllAsync();   
            var c = new ClientFormView { Clientes = clientes };
            return View(c);
        }

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction(nameof(Index));

               
                

            }
            
            try
            {
                await _clienteService.UpdateAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }


            
           


        }

        public async Task<IActionResult> DetailsAsync(int id)
        {

            var cliente = await _clienteService.FindByIdAsync(id);
            return View(cliente);

        }

        public async Task<IActionResult> Search(string nome)
        {
            if(nome == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var clientes = await _clienteService.FindByNameAsync(nome);
            var c = new ClientFormView { Clientes = clientes };
            return View(nameof(Index) ,c);  

        }
    }
}
