using AjaxControlToolkit;
using Microsoft.EntityFrameworkCore;
using Padaria.Models;
using Padaria.Services.Exceptions;

namespace Padaria.Services
{
    public class ProdutoService
    {

        private readonly PadariaContext _context;

        public ProdutoService(PadariaContext context)
        {
            _context = context;
        }

        public async Task<Produto> FindByIdAsync(int id)
        {

            return await _context.Produto.FirstOrDefaultAsync(pr => pr.Id == id);

        }

        public async Task<Produto> FindByCodeAsync(String codigo)
        {
            if(codigo.Length >=8)
            return await _context.Produto.FirstOrDefaultAsync(pr => pr.Codigo.Equals(codigo));

            else
                return await _context.Produto.FirstOrDefaultAsync(pr => pr.Id == int.Parse(codigo));

        }

        public async Task<List<Produto>> FindAllAsync()
        {
            return await _context.Produto.ToListAsync();

        }

        public async Task AddAsync(Produto produto)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateAsync(Produto produto)
        {
            _context.Update(produto);
            await _context.SaveChangesAsync();

        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var produto = await _context.Produto.FindAsync(id);
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
            }

            catch(DbUpdateException)
            {
                throw new IntegrityException("Esse produto não pode ser excluido, pois está contido em uma ou mais contas");
            }
        }

        public async Task<List<Produto>> SearchAsync(string nome)
        {
          return await _context.Produto.Where(p => p.Nome.Contains(nome)).ToListAsync();
        }

        public async Task<bool> AnyAsync(Produto produto)
        {
           return await _context.Produto.AnyAsync(e => e.Codigo == produto.Codigo && e.Id != produto.Id);
        }
    }
}
