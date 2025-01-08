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
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Categoria categoria)
        {
            var newCategoria = await _categoriaService.PostCategoria(categoria);
            return Ok(newCategoria);
        }

        [HttpPut]
        public IActionResult Put(string novoNome)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string nome)
        {
            var removedCategoria = _categoriaService.DeleteCategoria(nome);
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
