using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface IProdutoService
    {
        ICollection<Produto> GetProdutos();
        Produto PostProduto(Produto produto);
        Produto UpdateProduto(Produto produto, string novoNome);
        Produto DeleteProduto(string nome);
        List<Produto> ListByProdutoUrl(string produtoUrl);
    }

    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _dbContext;

        public ProdutoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Produto DeleteProduto(string nome)
        {
            throw new NotImplementedException();
        }

        public ICollection<Produto> GetProdutos()
        {
            throw new NotImplementedException();
        }

        public Produto PostProduto(Produto produto)
        {
            _dbContext.Produtos.Add(produto);
            _dbContext.SaveChanges();

            return produto;
        }

        public Produto UpdateProduto(Produto produto, string novoNome)
        {
            throw new NotImplementedException();
        }

        public List<Produto> ListByProdutoUrl(string produtoUrl)
        {
            var query = _dbContext.Produtos.Where(c => produtoUrl.Contains(c.Url)).ToList();

            return query;
        }
    }
}
