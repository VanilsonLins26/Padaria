using AjaxControlToolkit;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;
using Padaria.Models.Enums;

namespace Padaria.Services
{
    public class ContaService
    {
        private readonly PadariaContext _context;

        public ContaService(PadariaContext context)
        {
            _context = context;
        }

        public async Task<List<Conta>> FindAllAsync()
        {
           

            return await _context.Conta.Include(c => c.Produtos).Where(c => c.Status == StatusConta.Concluido).OrderByDescending(c => c.Data).ToListAsync();
        }

        public async Task AddAsync(Conta conta)
        {
            _context.Conta.Add(conta);  
           await _context.SaveChangesAsync();
        }

        public async Task<List<Conta>> SearchByDateAsync(DateTime dataInicial, DateTime? dataFinal)
        {
            if (dataFinal != null)
            {
                return await _context.Conta.Include(c => c.Produtos).Where(c => c.Data.Date >= dataInicial && c.Data.Date <= dataFinal).OrderByDescending(c => c.Data).ToListAsync();

            }
            else
            {
                return await _context.Conta.Include(c => c.Produtos).Where(c => c.Data.Date == dataInicial).OrderByDescending(c => c.Data).ToListAsync();


            }
        }

        public async Task<Conta> FindByIdAsync(int id)
        {
          return await _context.Conta.Include(c => c.Produtos).ThenInclude(c => c.Produto).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(Conta conta)
        {
            _context.Update(conta);    
            await _context.SaveChangesAsync();  
        }

        public async Task CleanAsync()
        {
            var contas = await _context.Conta.Where(c => c.Status == StatusConta.NaoFinalizado).ToListAsync();
            _context.RemoveRange(contas);
            await _context.SaveChangesAsync();
        }
    }
}
