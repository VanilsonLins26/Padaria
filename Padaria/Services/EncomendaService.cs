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
          return await _context.Encomenda.Where(e => e.Status == StatusConta.Andamento).Include(e => e.Cliente).Include(e => e.Produtos).OrderBy(e => e.Data).GroupBy(e => e.Data.Date).ToListAsync();
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
           return await _context.Encomenda.Where(e => e.Status == StatusConta.Concluido).Include(e => e.Cliente).Include(e => e.Produtos).OrderByDescending(e => e.Data).ToListAsync();
        }

        public async Task<List<Encomenda>> FindCompletedByDate(DateTime dataInicial, DateTime? dataFinal)
        {
            if (dataFinal != null)
            {
               return await _context.Encomenda.Include(c => c.Produtos).Include(e => e.Cliente).Where(e => e.Data.Date >= dataInicial && e.Data.Date <= dataFinal && e.Status == StatusConta.Concluido).OrderByDescending(e => e.Data).ToListAsync();

            }
            else
            {
                return await _context.Encomenda.Include(c => c.Produtos).Include(e => e.Cliente).Where(e => e.Data.Date == dataInicial && e.Status == StatusConta.Concluido).OrderByDescending(e => e.Data).ToListAsync();


            }

        }

       public async Task RemoveAsync(int id)
        {
           var encomenda = await FindById(id);
            encomenda.Cliente = null;   
            encomenda.Produtos = null;
            _context.Update(encomenda);
            _context.Remove(encomenda); 
           await _context.SaveChangesAsync();
        }
    }
}
