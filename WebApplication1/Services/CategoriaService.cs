using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface ICategoriaService
    {
        Task<ICollection<Categoria>> GetCategorias();
        Task<Categoria> PostCategoria(Categoria categoria);
        Categoria UpdateCategoria(Categoria categoria, string novoNome);
        Categoria DeleteCategoria(string nome);
        Task<List<Categoria>> ListByProduto();

        Task<List<Categoria>> ListByCategoriaUrl(string categoriaUrl);
    }

    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _dbContext;

        public CategoriaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Categoria>> GetCategorias()
        {
            var categorias = await _dbContext.Categorias.ToListAsync();
            return categorias;
        }
        public async Task<Categoria> PostCategoria(Categoria categoria)
        {
            _dbContext.Categorias.Add(categoria);
            await _dbContext.SaveChangesAsync();

            return categoria;
        }

        public Categoria DeleteCategoria(string nome)
        {
            throw new NotImplementedException();
        }

        public Categoria UpdateCategoria(Categoria categoria, string novoNome)
        {
            throw new NotImplementedException();
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
