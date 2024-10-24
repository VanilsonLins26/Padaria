using Microsoft.EntityFrameworkCore;
using Padaria.Models.Enums;
using Padaria.Models;

namespace Padaria.Services
{
    public class ProdutoContaService
    {

        private readonly PadariaContext _context;

        public ProdutoContaService(PadariaContext context)
        {
            _context = context;
        }

        public double ValorTotalProduto(ProdutoConta produtoConta)
        {
            double total = total = produtoConta.Produto.Preco * produtoConta.Quantidade; ;
            if (produtoConta.Produto.Tipo == Tipo.Unidade)
            {
                return total;   
            }
            
              return  total /= 1000;
            


        }

        public double ValorTotal(List<ProdutoConta> produtosConta)
        {
            return produtosConta.Sum(p => ValorTotalProduto(p));
        }

        public async Task<Produto> FindByCodeAsync(string codigo)
        {
           return await _context.Produto.FirstOrDefaultAsync(p => p.Codigo.Equals(codigo));
        }
    }

}

