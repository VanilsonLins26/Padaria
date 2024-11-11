using Microsoft.EntityFrameworkCore;
using Padaria.Models.Enums;
using Padaria.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Padaria.Services
{
    public class ProdutoContaService
    {

        private readonly PadariaContext _context;

        public ProdutoContaService(PadariaContext context)
        {
            _context = context;
        }





        public async Task<Produto> FindByCodeAsync(string codigo)
        {
            if (codigo.Length < 8)
                return await _context.Produto.FirstOrDefaultAsync(p => p.Id == int.Parse(codigo));

            else
                return await _context.Produto.FirstOrDefaultAsync(p => p.Codigo.Equals(codigo));
        }



        public async Task<ProdutoConta> FindAsync(int id)
        {
            return await _context.ProdutosConta.FirstOrDefaultAsync(pc => pc.ProdutoId == id);
        }

        public async Task<ProdutoConta> FindByIdAsync(int id)
        {
            return await _context.ProdutosConta.Include(pc => pc.Conta).FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task UpdateAsync(ProdutoConta pc)
        {
            _context.Update(pc);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(ProdutoConta pc)
        {
            
            _context.Remove(pc);
            await _context.SaveChangesAsync();  
        }
    }

}

