using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.Context;
using static WebApplication1.Services.PedidoService;

namespace WebApplication1.Services
{
    public interface IPedidoService
    {
        Task<ICollection<Pedido>> GetPedidos();
        Task<Pedido> PostPedido(Pedido pedido);
        Task<Pedido> UpdatePedido(int id, int novoId);
        Task<Pedido> DeletePedido(int id);
        Task<List<PedidoWithQuantidade>> ListWithAuth();
    }

    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public PedidoService(AppDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private async Task<Pedido> FindPedidoById(int id)
        {
            return await _dbContext.Pedidos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ICollection<Pedido>> GetPedidos()
        {
            return await _dbContext.Pedidos.ToListAsync();
        }

        public async Task<Pedido> PostPedido(Pedido pedido)
        {
            try
            {
                if (await _dbContext.Pedidos.AnyAsync(p => p.Id == pedido.Id))
                {
                    _logger.LogWarning("Pedido with id {id} already exists.", pedido.Id);
                    return null;
                }

                _dbContext.Pedidos.Add(pedido);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Pedido {id} created successfully.", pedido.Id);

                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating pedido {id}", pedido.Id);
                throw;
            }
        }

        public async Task<Pedido> DeletePedido(int id)
        {
            try
            {
                var pedido = await FindPedidoById(id);

                if (pedido == null)
                {
                    _logger.LogWarning("Pedido with id {id} not found.", id);
                    return null;
                }

                _dbContext.Pedidos.Remove(pedido);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Pedido {id} deleted successfully.", id);

                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting pedido {id}", id);
                throw;
            }
        }

        public async Task<Pedido> UpdatePedido(int id, int novoId)
        {
            try
            {
                var pedido = await FindPedidoById(id);

                if (pedido == null)
                {
                    _logger.LogWarning("Pedido with id {id} not found.", id);
                    return null;
                }

                pedido.UsuarioId = novoId;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Pedido {id} updated successfully.", id);

                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating pedido{id}", id);
                throw;
            }
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
