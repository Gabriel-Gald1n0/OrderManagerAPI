using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Models;
using OrderManagerAPI.Dto.TypesProduct;
using OrderManagerAPI.Services.TypesProduct;

namespace OrderManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesProductsController : ControllerBase
    {
        private readonly TypesProductInterface _typesProductInterface;

        public TypesProductsController(TypesProductInterface typesProductInterface)
        {
            _typesProductInterface = typesProductInterface;
        }

        // GET: api/TypesProducts
        [HttpGet("getTipoProduto")]
        public async Task<ActionResult<ResponseModel<List<TipoProduto>>>> GetTipoProdutos()
        {
            var tipoProdutos = await _typesProductInterface.GetTipoProduto();
            if(!tipoProdutos.Status && tipoProdutos.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = tipoProdutos.Mensagem });
            }
            return Ok(tipoProdutos);
        } 
        
        // GET: api/TypesProducts/5
        [HttpGet("getTipoProduto/{id}")]
        public async Task<ActionResult<ResponseModel<TipoProduto>> > GetTipoProduto(int id)
        {
            var tipoProduto = await _typesProductInterface.GetTipoProduto(id);
            if(!tipoProduto.Status && tipoProduto.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = tipoProduto.Mensagem });
            }
            return Ok(tipoProduto);
        } 
       
        // PUT: api/TypesProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("editar/tipoProduto/{id}")]
        public async Task<ActionResult<ResponseModel<List<TipoProduto>>> > PutTipoProduto(int id, TypesProductEditDto typesProductEditDto)
        {
            var tipoProduto = await _typesProductInterface.PutTipoProduto(id, typesProductEditDto);
            if(!tipoProduto.Status && tipoProduto.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = tipoProduto.Mensagem  });
            }
            return Ok(tipoProduto);
        }

        // POST: api/TypesProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("cadastrar/tipoProduto")]
        public async Task<ActionResult<ResponseModel<TipoProduto>>> PostTipoProduto(TypesProductCreateDto typesProductCreateDto)
        {
            var tipoProduto = await _typesProductInterface.PostTipoProduto(typesProductCreateDto);
            return Ok(tipoProduto);
        }

        // DELETE: api/TypesProducts/5
        [HttpDelete("deletar/tipoProduto/{id}")]
        public async Task<ActionResult<ResponseModel<List<TipoProduto>>> > DeleteTipoProduto(int id)
        {
            var tipoProduto = await _typesProductInterface.DeleteTipoProduto(id);
            if(!tipoProduto.Status && tipoProduto.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = tipoProduto.Mensagem  });
            }
            return Ok(tipoProduto);
        }
    }
}
