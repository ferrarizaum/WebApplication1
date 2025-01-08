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

            if (pedidos == null || pedidos.Count == 0)
            {
                return NoContent();
            }

            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            if (pedido == null)
            {
                return BadRequest("Pedido data cannot be null.");
            }

            var newPedido = await _pedidoService.PostPedido(pedido);

            if (newPedido == null)
            {
                return Conflict("Pedido with the same id already exists.");
            }

            return CreatedAtAction(nameof(Get), new { id = newPedido.Id }, newPedido);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] int id, [FromQuery] int novoId)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest("Invalid Id.");
            }

            var updatedPedido = await _pedidoService.UpdatePedido(id, novoId);

            if (updatedPedido == null)
            {
                return NotFound("Pedido not found.");
            }

            return Ok(updatedPedido);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest("Invalid Id.");
            }

            var removedPedido = await _pedidoService.DeletePedido(id);

            if (removedPedido == null)
            {
                return NotFound("Pedido not found.");
            }

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
