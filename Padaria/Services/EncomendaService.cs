using Microsoft.AspNetCore.Mvc;
using Padaria.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;

namespace Padaria.Services
{
    public class EncomendaService
    {
        private readonly PadariaContext _context;

        public EncomendaService(PadariaContext context)
        {
            _context = context;
        }

        public async Task<List<IGrouping<DateTime, Encomenda>>> GroupFindAsync()
        {
          return await _context.Encomenda.Where(e => e.Status == Status.Andamento).Include(e => e.Cliente).OrderBy(e => e.Data).GroupBy(e => e.Data.Date).ToListAsync();
        }

        public async Task AddAsync(Encomenda encomenda)
        {
            _context.Add(encomenda);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Encomenda encomenda)
        {
            _context.Update(encomenda);
            await _context.SaveChangesAsync();
        }

        public async Task<Encomenda> FindById(int id)
        {
            return await _context.Encomenda.Include(e => e.Cliente).Include(e => e.Produtos).ThenInclude(p => p.Produto).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Encomenda>> FindAllCompleted()
        {
           return await _context.Encomenda.Where(e => e.Status == Status.Concluido).Include(e => e.Cliente).OrderByDescending(e => e.Data).ToListAsync();
        }

        public async Task<List<Encomenda>> FindCompletedByDate(DateTime dataInicial, DateTime? dataFinal)
        {
            if (dataFinal != null)
            {
               return await _context.Encomenda.Where(e => e.Data.Date >= dataInicial && e.Data.Date <= dataFinal && e.Status == Status.Concluido).OrderByDescending(e => e.Data).ToListAsync();

            }
            else
            {
                return await _context.Encomenda.Where(e => e.Data.Date == dataInicial && e.Status == Status.Concluido).OrderByDescending(e => e.Data).ToListAsync();


            }

        }
    }
}
