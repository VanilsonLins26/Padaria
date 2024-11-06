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
            var encomendas = _context.Conta.Where(e => (e as Encomenda).Status == Status.Andamento);

            return await _context.Conta.Except(encomendas).OrderByDescending(c => c.Data).ToListAsync();
        }

        public async Task AddConta(Conta conta)
        {
            _context.Conta.Add(conta);  
           await _context.SaveChangesAsync();
        }

        public async Task<List<Conta>> SearchByDateAsync(DateTime dataInicial, DateTime? dataFinal)
        {
            if (dataFinal != null)
            {
                return await _context.Conta.Where(c => c.Data.Date >= dataInicial && c.Data.Date <= dataFinal).OrderByDescending(c => c.Data).ToListAsync();

            }
            else
            {
                return await _context.Conta.Where(c => c.Data.Date == dataInicial).OrderByDescending(c => c.Data).ToListAsync();


            }
        }

        public async Task<Conta> FindByIdAsync(int id)
        {
          return await _context.Conta.Include(c => c.Produtos).ThenInclude(c => c.Produto).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
