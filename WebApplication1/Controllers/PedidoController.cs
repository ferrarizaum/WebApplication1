using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pedidos = await _pedidoService.GetPedidos();
            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            var newPedido = await _pedidoService.PostPedido(pedido);
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
            var removedPedido = _pedidoService.DeletePedido(nome);
            return Ok(removedPedido);
        }

        [Authorize]
        [HttpGet]
        [Route("ListWithAuth")]
        public async Task<IActionResult> ListWithAuth()
        {
            var query = await _pedidoService.ListWithAuth();
            return Ok(query);
        }

        // 8
        [Authorize]
        [HttpPost]
        [Route("PostWithAuth")]
        public async Task<IActionResult> PostWithAuth([FromBody] Pedido pedido)
        {
            var newPedido = await _pedidoService.PostPedido(pedido);
            return Ok(newPedido);
        }
    }
}
