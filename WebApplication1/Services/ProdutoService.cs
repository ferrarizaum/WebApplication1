using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface IProdutoService
    {
        Task<ICollection<Produto>> GetProdutos();
        Task<Produto> PostProduto(Produto produto);
        Produto UpdateProduto(Produto produto, string novoNome);
        Produto DeleteProduto(string nome);
        Task<List<Produto>> ListByProdutoUrl(string produtoUrl);
    }

    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _dbContext;

        public ProdutoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Produto>> GetProdutos()
        {
            var produtos = await _dbContext.Produtos.ToListAsync();
            return produtos;
        }
        public async Task<Produto> PostProduto(Produto produto)
        {
            _dbContext.Produtos.Add(produto);
            await _dbContext.SaveChangesAsync();

            return produto;
        }

        public Produto DeleteProduto(string nome)
        {
            throw new NotImplementedException();
        }

        public Produto UpdateProduto(Produto produto, string novoNome)
        {
            throw new NotImplementedException();
        }

        // 6
        public async Task<List<Produto>> ListByProdutoUrl(string produtoUrl)
        {
            var query = await _dbContext.Produtos.Where(c => produtoUrl.Contains(c.Url)).ToListAsync();

            return query;
        }
    }
}
