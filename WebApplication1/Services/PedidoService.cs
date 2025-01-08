using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using static WebApplication1.Services.PedidoService;

namespace WebApplication1.Services
{
    public interface IPedidoService
    {
        Task<ICollection<Pedido>> GetPedidos();
        Task<Pedido> PostPedido(Pedido pedido);
        Pedido UpdatePedido(Pedido pedido, string novoNome);
        Pedido DeletePedido(string nome);
        Task<List<PedidoWithQuantidade>> ListWithAuth();
    }

    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _dbContext;

        public PedidoService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<Pedido>> GetPedidos()
        {
            var pedidos = await _dbContext.Pedidos.ToListAsync();
            return pedidos;
        }
        public async Task<Pedido> PostPedido(Pedido pedido)
        {
            _dbContext.Pedidos.Add(pedido);
            await _dbContext.SaveChangesAsync();

            return pedido;
        }

        public Pedido DeletePedido(string nome)
        {
            throw new NotImplementedException();
        }

        public Pedido UpdatePedido(Pedido pedido, string novoNome)
        {
            throw new NotImplementedException();
        }

        // 9
        public async Task<List<PedidoWithQuantidade>> ListWithAuth()
        {
            var query = from pedido in _dbContext.Pedidos
                        join pedidoItem in _dbContext.PedidoItem
                        on pedido.Id equals pedidoItem.PedidoId
                        select new PedidoWithQuantidade
                        {
                            PedidoId = pedido.Id,
                            Quantidade = pedidoItem.Quantidade
                        };

            return await query.ToListAsync();
        }

        public class PedidoWithQuantidade
        {
            public int PedidoId { get; set; }
            public int Quantidade { get; set; }
        }
    }
}
