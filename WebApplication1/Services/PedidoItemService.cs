using static WebApplication1.Services.PedidoService;
using WebApplication1.Context;

namespace WebApplication1.Services
{
    public interface IPedidoItemService
    {
        ICollection<PedidoItem> GetPedidosItem();
        PedidoItem PostPedidoItem(PedidoItem pedidoItem);
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
        public ICollection<PedidoItem> GetPedidosItem()
        {
            throw new NotImplementedException();
        }

        public PedidoItem DeletePedidoItem(string nome)
        {
            throw new NotImplementedException();
        }


        public PedidoItem PostPedidoItem(PedidoItem pedidoItem)
        {
            _dbContext.PedidoItem.Add(pedidoItem);
            _dbContext.SaveChanges();

            return pedidoItem;
        }

        public PedidoItem UpdatePedidoItem(PedidoItem pedidoItem, string novoNome)
        {
            throw new NotImplementedException();
        }

    }
}
