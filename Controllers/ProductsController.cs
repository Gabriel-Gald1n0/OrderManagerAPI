using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Models;
using WebAPI.Context;
using OrderManagerAPI.Dto.Product;
using OrderManagerAPI.Services.Product;

namespace OrderManagerAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductInterface _productInterface;

        public ProductsController(ProductInterface productInterface)
        {
            _productInterface = productInterface;
        }

        // GET: api/Products
        [HttpGet("getProdutos")]
        public async Task<ActionResult<ResponseModel<List<Produtos>>>> GetProdutos()
        {
            var produtos = await _productInterface.GetProdutos();
            if(!produtos.Status && produtos.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = produtos.Mensagem  });
            }
            return Ok(produtos);
        }

        // GET: api/Products/5
        [HttpGet("getProdutos/{id}")]
        public async Task<ActionResult<ResponseModel<Produtos>>> GetProdutos(int id)
        {
            var produto = await _productInterface.GetProdutos(id);
            if(!produto.Status && produto.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = produto.Mensagem  });
            }
            return Ok(produto);
        }

        // PUT: api/Products/5
        [HttpPut("editar/produto/{id}")]
        public async Task<ActionResult<ResponseModel<List<Produtos>>>> PutProdutos(int id, ProductEditDto produtos)
        {
            var produto = await _productInterface.PutProdutos(id, produtos);
            if(!produto.Status && produto.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = produto.Mensagem });
            }
            return Ok(produto);
        }

        // POST: api/Products
        [HttpPost("cadastrar/produto")]
        public async Task<ActionResult<ResponseModel<Produtos>>> PostProdutos(ProductCreateDto produtos)
        {
            var produto = await _productInterface.PostProdutos(produtos);
            return Ok(produto);
        }

        // DELETE: api/Products/5
        [HttpDelete("deletar/produto/{id}")]
        public async Task<ActionResult<ResponseModel<List<Produtos>>>> DeleteProdutos(int id)
        {
            var produto = await _productInterface.DeleteProdutos(id);
            if(!produto.Status && produto.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = produto.Mensagem });
            }
            return Ok(produto);
        }
    }
}
