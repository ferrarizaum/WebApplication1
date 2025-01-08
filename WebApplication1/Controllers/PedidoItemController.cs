using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoItemController : Controller
    {
        private readonly IPedidoItemService _pedidoItemService;

        public PedidoItemController(IPedidoItemService pedidoItemService)
        {
            _pedidoItemService = pedidoItemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _pedidoItemService.GetPedidosItem();
            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidoItem pedidoItem)
        {
            var newPedido = await _pedidoItemService.PostPedidoItem(pedidoItem);
            return Ok(newPedido);
        }

        [HttpPut]
        public IActionResult Put(string novoNome)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string nome)
        {
            var removedPedidoItem = _pedidoItemService.DeletePedidoItem(nome);
            return Ok(removedPedidoItem);
        }
    }
}
