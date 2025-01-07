using WebApplication1.Context;
using static WebApplication1.Services.PedidoService;

namespace WebApplication1.Services
{
    public interface IPedidoService
    {
        ICollection<Pedido> GetPedidos();
        Pedido PostPedido(Pedido pedido);
        Pedido UpdatePedido(Pedido pedido, string novoNome);
        Pedido DeletePedido(string nome);
        List<PedidoWithQuantidade> ListWithAuth();
    }

    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _dbContext;

        public PedidoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Pedido DeletePedido(string nome)
        {
            throw new NotImplementedException();
        }

        public ICollection<Pedido> GetPedidos()
        {
            var pedidos = _dbContext.Pedidos.ToList();
            return pedidos;
        }

        public Pedido PostPedido(Pedido pedido)
        {
            _dbContext.Pedidos.Add(pedido);
            _dbContext.SaveChanges();

            return pedido;
        }

        public Pedido UpdatePedido(Pedido pedido, string novoNome)
        {
            throw new NotImplementedException();
        }

        public List<PedidoWithQuantidade> ListWithAuth()
        {
            var query = from pedido in _dbContext.Pedidos
                        join pedidoItem in _dbContext.PedidoItem
                        on pedido.Id equals pedidoItem.PedidoId
                        select new PedidoWithQuantidade
                        {
                            PedidoId = pedido.Id,
                            Quantidade = pedidoItem.Quantidade
                        };

            return query.ToList();
        }

        public class PedidoWithQuantidade
        {
            public int PedidoId { get; set; }
            public int Quantidade { get; set; }
        }
    }
}
