using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _categoriaService.GetCategorias();

            if (categorias == null || categorias.Count == 0)
            {
                return NoContent();
            }

            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Categoria data cannot be null.");
            }

            var newCategoria = await _categoriaService.PostCategoria(categoria);

            if (newCategoria == null)
            {
                return Conflict("Categoria with the same id already exists.");
            }

            return CreatedAtAction(nameof(Get), new { nome = newCategoria.Nome }, newCategoria);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] string nome, [FromQuery] string novoNome)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(novoNome))
            {
                return BadRequest("Invalid Nome.");
            }

            var updatedCategoria = await _categoriaService.UpdateCategoria(nome, novoNome);

            if (updatedCategoria == null)
            {
                return NotFound("Caegoria not found.");
            }

            return Ok(updatedCategoria);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("Invalid Nome.");
            }

            var removedCategoria = await _categoriaService.DeleteCategoria(nome);

            if (removedCategoria == null)
            {
                return NotFound("Categoria not found.");
            }

            return Ok(removedCategoria);
        }

        [HttpGet]
        [Route("ListByProduto")]
        public async Task<IActionResult> ListByProduto()
        {
            var query = await _categoriaService.ListByProduto();
            return Ok(query);
        }

        [HttpGet]
        [Route("ListByCategoriaUrl")]
        public async Task<IActionResult> ListByCategoriaUrl(string categoriaUrl)
        {
            var query = await _categoriaService.ListByCategoriaUrl(categoriaUrl);
            return Ok(query);
        }
    }
}
