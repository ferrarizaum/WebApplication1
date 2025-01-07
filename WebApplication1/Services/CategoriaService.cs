using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface ICategoriaService
    {
        ICollection<Categoria> GetCategorias();
        Categoria PostCategoria(Categoria categoria);
        Categoria UpdateCategoria(Categoria categoria, string novoNome);
        Categoria DeleteCategoria(string nome);
        List<Categoria> ListByProduto();

        List<Categoria> ListByCategoriaUrl(string categoriaUrl);
    }

    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _dbContext;

        public CategoriaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Categoria DeleteCategoria(string nome)
        {
            throw new NotImplementedException();
        }

        public ICollection<Categoria> GetCategorias()
        {
            var categorias = _dbContext.Categorias.ToList();
            return categorias;
        }

        public Categoria PostCategoria(Categoria categoria)
        {
            _dbContext.Categorias.Add(categoria);
            _dbContext.SaveChanges();

            return categoria;
        }

        public Categoria UpdateCategoria(Categoria categoria, string novoNome)
        {
            throw new NotImplementedException();
        }

        public List<Categoria> ListByProduto()
        {
            var products = _dbContext.Produtos.Where(p => p.Quantidade > 0 && p.Ativo == true).ToList();

            var productIds = products.Select(p => p.Id).ToList();

            var query = _dbContext.Categorias.Where(c => productIds.Contains(c.Id)).ToList();

            return query;
        }

        public List<Categoria> ListByCategoriaUrl(string categoriaUrl)
        {
            var query = _dbContext.Categorias.Where(c => categoriaUrl.Contains(c.Url)).ToList();

            return query;
        }
    }
}
