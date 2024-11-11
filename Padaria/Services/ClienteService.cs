using Microsoft.EntityFrameworkCore;
using Padaria.Models;

namespace Padaria.Services
{
    public class ClienteService
    {

        private readonly PadariaContext _context;

        public ClienteService(PadariaContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> FindAllAsync()
        {
            return await _context.Cliente.ToListAsync();    
        }

        public async Task InsertAsync(Cliente cliente)
        {
             _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Update(cliente);
           await _context.SaveChangesAsync();
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            var cliente = await _context.Cliente.Include(c => c.Encomendas.OrderByDescending(e => e.Data)).ThenInclude(e => e.Produtos).FirstOrDefaultAsync(c => c.Id == id);
            
            return cliente;
        }

        public async Task<List<Cliente>> FindByNameAsync(string nome)
        {
          return await _context.Cliente.Where(c => c.Nome.Contains(nome)).ToListAsync();
        }

        
    }
}
