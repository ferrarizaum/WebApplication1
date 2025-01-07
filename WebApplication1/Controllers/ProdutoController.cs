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
            var produtos = _produtoService.GetProdutos();
            return Ok(produtos);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            var newProduto = _produtoService.PostProduto(produto);
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
        public IActionResult ListByProdutoUrl(string produtoUrl)
        {
            var query = _produtoService.ListByProdutoUrl(produtoUrl);
            return Ok(query);
        }
    }
}
