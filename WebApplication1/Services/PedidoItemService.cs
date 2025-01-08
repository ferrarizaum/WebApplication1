using static WebApplication1.Services.PedidoService;
using WebApplication1.Context;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public interface IPedidoItemService
    {
        Task<ICollection<PedidoItem>> GetPedidosItem();
        Task<PedidoItem> PostPedidoItem(PedidoItem pedidoItem);
        PedidoItem UpdatePedidoItem(PedidoItem pedidoItem, string novoNome);
        PedidoItem DeletePedidoItem(string nome);
    }

    public class PedidoItemService : IPedidoItemService
    {
        private readonly AppDbContext _dbContext;

        public PedidoItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ICollection<PedidoItem>> GetPedidosItem()
        {
            var pedidosItem = await _dbContext.PedidoItem.ToListAsync();
            return pedidosItem;
        }
        public async Task<PedidoItem> PostPedidoItem(PedidoItem pedidoItem)
        {
            _dbContext.PedidoItem.Add(pedidoItem);
            await _dbContext.SaveChangesAsync();

            return pedidoItem;
        }

        public PedidoItem DeletePedidoItem(string nome)
        {
            throw new NotImplementedException();
        }

        public PedidoItem UpdatePedidoItem(PedidoItem pedidoItem, string novoNome)
        {
            throw new NotImplementedException();
        }

    }
}
