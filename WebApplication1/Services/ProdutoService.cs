using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface IProdutoService
    {
        Task<ICollection<Produto>> GetProdutos();
        Task<Produto> PostProduto(Produto produto);
        Task<Produto> UpdateProduto(string nome, string novoNome);
        Task<Produto> DeleteProduto(string nome);
        Task<List<Produto>> ListByProdutoUrl(string produtoUrl);
    }

    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public ProdutoService(AppDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private async Task<Produto> FindProdutoByNome(string nome)
        {
            return await _dbContext.Produtos.FirstOrDefaultAsync(p => p.Nome == nome);
        }

        public async Task<ICollection<Produto>> GetProdutos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        public async Task<Produto> PostProduto(Produto produto)
        {
            try
            {
                if (await _dbContext.Produtos.AnyAsync(p => p.Nome == produto.Nome))
                {
                    _logger.LogWarning("Produto with nome {nome} already exists.", produto.Nome);
                    return null;
                }

                _dbContext.Produtos.Add(produto);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Produto {Nome} created successfully.", produto.Nome);

                return produto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating produto {Nome}", produto.Nome);
                throw;
            }
        }

        public async Task<Produto> DeleteProduto(string nome)
        {
            try
            {
                var produto = await FindProdutoByNome(nome);

                if (produto == null)
                {
                    _logger.LogWarning("Produto with nome {nome} not found.", nome);
                    return null;
                }

                _dbContext.Produtos.Remove(produto);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Produto {nome} deleted successfully.", nome);

                return produto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting produto {nome}", nome);
                throw;
            }
        }

        public async Task<Produto> UpdateProduto(string nome, string novoNome)
        {
            try
            {
                var produto = await FindProdutoByNome(nome);

                if (produto == null)
                {
                    _logger.LogWarning("Produto with nome {nome} not found.", nome);
                    return null;
                }

                produto.Nome = novoNome;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Produto {nome} updated successfully.", nome);

                return produto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating produto {nome}", nome);
                throw;
            }
        }

        // 6
        public async Task<List<Produto>> ListByProdutoUrl(string produtoUrl)
        {
            var query = await _dbContext.Produtos.Where(c => produtoUrl.Contains(c.Url)).ToListAsync();

            return query;
        }
    }
}
