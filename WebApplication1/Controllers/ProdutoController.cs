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
            return Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            var newProduto = await _produtoService.PostProduto(produto);
            return Ok(newProduto);
        }

        [HttpPut]
        public IActionResult Put(string novoNome)
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(string nome)
        {
            var removedProduto = _produtoService.DeleteProduto(nome);
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
