using static WebApplication1.Services.PedidoService;
using WebApplication1.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Services
{
    public interface IPedidoItemService
    {
        Task<ICollection<PedidoItem>> GetPedidosItem();
        Task<PedidoItem> PostPedidoItem(PedidoItem pedidoItem);
        Task<PedidoItem> UpdatePedidoItem(int id, int qtd);
        Task<PedidoItem> DeletePedidoItem(int id);
    }

    public class PedidoItemService : IPedidoItemService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UserService> _logger;

        public PedidoItemService(AppDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private async Task<PedidoItem> FindPedidoItemById(int id)
        {
            return await _dbContext.PedidoItem.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ICollection<PedidoItem>> GetPedidosItem()
        {
            return await _dbContext.PedidoItem.ToListAsync();
        }
        public async Task<PedidoItem> PostPedidoItem(PedidoItem pedidoItem)
        {
            try
            {
                if (await _dbContext.PedidoItem.AnyAsync(p => p.Id == pedidoItem.Id))
                {
                    _logger.LogWarning("PedidoItem with id {id} already exists.", pedidoItem.Id);
                    return null;
                }

                _dbContext.PedidoItem.Add(pedidoItem);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("PedidoItem {id} created successfully.", pedidoItem.Id);

                return pedidoItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating pedidoItem {id}", pedidoItem.Id);
                throw;
            }
        }

        public async Task<PedidoItem> DeletePedidoItem(int id)
        {
            try
            {
                var pedidoItem = await FindPedidoItemById(id);

                if (pedidoItem == null)
                {
                    _logger.LogWarning("PedidoItem with id {id} not found.", id);
                    return null;
                }

                _dbContext.PedidoItem.Remove(pedidoItem);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Pedido Item {id} deleted successfully.", id);

                return pedidoItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting pedido Item {id}", id);
                throw;
            }
        }

        public async Task<PedidoItem> UpdatePedidoItem(int id, int qtd)
        {
            try
            {
                var pedidoItem = await FindPedidoItemById(id);

                if (pedidoItem == null)
                {
                    _logger.LogWarning("Pedido Item with id {id} not found.", id);
                    return null;
                }

                pedidoItem.Quantidade = qtd;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Pedido Item {id} updated successfully.", id);

                return pedidoItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating pedido Item {id}", id);
                throw;
            }
        }

    }
}
