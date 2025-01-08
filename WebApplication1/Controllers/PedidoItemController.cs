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
            var pedidosItem = await _pedidoItemService.GetPedidosItem();

            if (pedidosItem == null || pedidosItem.Count == 0)
            {
                return NoContent();
            }

            return Ok(pedidosItem);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PedidoItem pedidoItem)
        {
            if (pedidoItem == null)
            {
                return BadRequest("Pedido Item data cannot be null.");
            }

            var newPedidoItem = await _pedidoItemService.PostPedidoItem(pedidoItem);

            if (newPedidoItem == null)
            {
                return Conflict("Pedido Item with the same id already exists.");
            }

            return CreatedAtAction(nameof(Get), new { id = newPedidoItem.Id }, newPedidoItem);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] int id, [FromQuery] int qtd)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest("Invalid Id.");
            }

            var updatedPedidoItem = await _pedidoItemService.UpdatePedidoItem(id, qtd);

            if (updatedPedidoItem == null)
            {
                return NotFound("Pedido Item not found.");
            }

            return Ok(updatedPedidoItem);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest("Invalid Id.");
            }

            var removedPedidoItem = await _pedidoItemService.DeletePedidoItem(id);

            if (removedPedidoItem == null)
            {
                return NotFound("Pedido Item not found.");
            }

            return Ok(removedPedidoItem);
        }
    }
}
