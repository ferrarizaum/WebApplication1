using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface ICategoriaService
    {
        Task<ICollection<Categoria>> GetCategorias();
        Task<Categoria> PostCategoria(Categoria categoria);
        Task<Categoria> UpdateCategoria(string nome, string novoNome);
        Task<Categoria> DeleteCategoria(string nome);
        Task<List<Categoria>> ListByProduto();

        Task<List<Categoria>> ListByCategoriaUrl(string categoriaUrl);
    }

    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public CategoriaService(AppDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private async Task<Categoria> FindCategoriaByNome(string nome)
        {
            return await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<ICollection<Categoria>> GetCategorias()
        {
            return await _dbContext.Categorias.ToListAsync();
        }
        public async Task<Categoria> PostCategoria(Categoria categoria)
        {
            try
            {
                if (await _dbContext.Categorias.AnyAsync(c => c.Nome == categoria.Nome))
                {
                    _logger.LogWarning("Categoria with nome {nome} already exists.", categoria.Nome);
                    return null;
                }

                _dbContext.Categorias.Add(categoria);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Categoria {nome} created successfully.", categoria.Nome);

                return categoria;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating categoria {nome}", categoria.Nome);
                throw;
            }
        }

        public async Task<Categoria> DeleteCategoria(string nome)
        {
            try
            {
                var categoria = await FindCategoriaByNome(nome);

                if (categoria == null)
                {
                    _logger.LogWarning("Categoria with nome {nome} not found.", nome);
                    return null;
                }

                _dbContext.Categorias.Remove(categoria);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Categoria {nome} deleted successfully.", nome);

                return categoria;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting categoria {nome}", nome);
                throw;
            }
        }

        public async Task<Categoria> UpdateCategoria(string nome, string novoNome)
        {
            try
            {
                var categoria = await FindCategoriaByNome(nome);

                if (categoria == null)
                {
                    _logger.LogWarning("Categoria with nome {nome} not found.", nome);
                    return null;
                }

                categoria.Nome = novoNome;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Categoria {nome} updated successfully.", nome);

                return categoria;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating categoria {nome}", nome);
                throw;
            }
        }

        // 4
        public async Task<List<Categoria>> ListByProduto()
        {
            var products = await _dbContext.Produtos.Where(p => p.Quantidade > 0 && p.Ativo == true).ToListAsync();

            var productIds = products.Select(p => p.Id).ToList();

            var query = await _dbContext.Categorias.Where(c => productIds.Contains(c.Id)).ToListAsync();

            return query;
        }

        // 5
        public async Task<List<Categoria>> ListByCategoriaUrl(string categoriaUrl)
        {
            var query = await _dbContext.Categorias.Where(c => categoriaUrl.Contains(c.Url)).ToListAsync();

            return query;
        }
    }
}
