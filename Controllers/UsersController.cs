using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using OrderManagerAPI.Models;
using OrderManagerAPI.Dto.User;
using OrderManagerAPI.Services.User;

namespace OrderManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserInterface _userInterface;
        public UsersController(UserInterface userInterface)
        {
            _userInterface = userInterface;
        }
       

        // GET: api/Users
        [HttpGet("getClientes")]
        public async Task<ActionResult<ResponseModel<List<Clientes>>>>  
        GetClientes()
        {
            var clientes = await _userInterface.GetClientes();
            if(!clientes.Status && clientes.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = clientes.Mensagem  });
            }

            return Ok(clientes);
        }

        // GET: api/Users/5
        [HttpGet("getClientes/{id}")]
        public async Task<ActionResult<ResponseModel<Clientes>>>  
        GetClientes(int id)
        {
            var cliente = await _userInterface.GetClientes(id);

            if(!cliente.Status && cliente.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = cliente.Mensagem  });
            }

            return Ok(cliente);
        }

        // PUT: api/Users/5
        [HttpPut("editar/cliente/{id}")]
        public async Task<ActionResult<ResponseModel<List<Clientes>>>> 
        PutClientes(int id, UserEditDto clientes)
        {
            var cliente = await _userInterface.PutClientes(id, clientes);
            if(!cliente.Status && cliente.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = cliente.Mensagem  });
            }
            return Ok(cliente);
        }

        // POST: api/Users
        [HttpPost("cadastrar/cliente")]
        public async Task<ActionResult<ResponseModel<Clientes>>>  PostClientes(UserCreateDto clientes)
        {
            var cliente = await _userInterface.PostClientes(clientes);
            return Ok(cliente);
        }

        // DELETE: api/Users/5
        [HttpDelete("deletar/cliente/{id}")]
        public async Task<ActionResult<ResponseModel<List<Clientes>>>> 
        DeleteClientes(int id)
        {
            var cliente = await _userInterface.DeleteClientes(id);
            if(!cliente.Status && cliente.StatusCode == 404)
            {
                return NotFound(new { status = 404, erros = cliente.Mensagem  });
            }
            return Ok(cliente);
        }
    }
}
