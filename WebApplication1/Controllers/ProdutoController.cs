using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var produtos = await _produtoService.GetProdutos();

            if (produtos == null || produtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            if (produto == null)
            {
                return BadRequest("Produto data cannot be null.");
            }

            var newProduto = await _produtoService.PostProduto(produto);

            if (newProduto == null)
            {
                return Conflict("Produto with the same nome already exists.");
            }

            return CreatedAtAction(nameof(Get), new { nome = newProduto.Nome }, newProduto);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromQuery] string nome, [FromQuery] string novoNome)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(novoNome))
            {
                return BadRequest("nome and novoNome cannot be null or empty.");
            }

            var updatedProduto = await _produtoService.UpdateProduto(nome, novoNome);

            if (updatedProduto == null)
            {
                return NotFound("Produto not found.");
            }

            return Ok(updatedProduto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                return BadRequest("Nome cannot be null or empty.");
            }

            var removedProduto = await _produtoService.DeleteProduto(nome);

            if (removedProduto == null)
            {
                return NotFound("Produto not found.");
            }

            return Ok(removedProduto);
        }

        [HttpGet]
        [Route("ListByProdutoUrl")]
        public async Task<IActionResult> ListByProdutoUrl(string produtoUrl)
        {
            var query = await _produtoService.ListByProdutoUrl(produtoUrl);
            return Ok(query);
        }
    }
}
