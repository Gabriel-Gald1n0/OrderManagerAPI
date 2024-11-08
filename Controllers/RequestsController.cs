using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using OrderManagerAPI.Models;
using OrderManagerAPI.Dto.Request;
using OrderManagerAPI.Services.Request;
using Azure.Core;

namespace OrderManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly RequestInterface _requestInterface;

        public RequestsController(RequestInterface requestInterface)
        {
            _requestInterface = requestInterface;
        }

        // GET: api/Requests
        [HttpGet("getPedidos")]
        public async Task<ActionResult<ResponseModel<List<Pedidos>>>> GetPedidos()
        {
            var pedidos = await _requestInterface.GetPedidos();
            if(!pedidos.Status && pedidos.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = pedidos.Mensagem  });
            }
            return Ok(pedidos);
        }

        // GET: api/Requests/5
        [HttpGet("getPedidos/{id}")]
        public async Task<ActionResult<ResponseModel<Pedidos>>> 
        GetPedidos(int id)
        {
            var pedido = await _requestInterface.GetPedidos(id);
            if(!pedido.Status && pedido.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = pedido.Mensagem });
            }
            return Ok(pedido);
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("editar/pedido/{id}")]
        public async Task<ActionResult<ResponseModel<List<Pedidos>>>>
        PutPedidos(int id, RequestEditDto pedidos)
        {
            var pedido = await _requestInterface.PutPedidos(id, pedidos);
            if(!pedido.Status && pedido.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = pedido.Mensagem  });
            }
            return Ok(pedido);
        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("cadastrar/pedido")]
        public async Task<ActionResult<ResponseModel<Pedidos>>>
        PostPedidos(RequestCreateDto pedidos)
        {
            var pedido = await _requestInterface.PostPedidos(pedidos);
            return Ok(pedido);
        }

        // DELETE: api/Requests/5
        [HttpDelete("deletar/pedido/{id}")]
        public async Task<ActionResult<ResponseModel<List<Pedidos>>>>
        DeletePedidos(int id)
        {
            var pedido = await _requestInterface.DeletePedidos(id);
            if(!pedido.Status && pedido.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = pedido.Mensagem  });
            }
            return Ok(pedido);
        }

    }
}
