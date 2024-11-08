using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Models;
using WebAPI.Context;
using OrderManagerAPI.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OrderManagerAPI.Services.User
{
    public class UserService : UserInterface
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<Clientes>>> GetClientes()
        {
            ResponseModel<List<Clientes>> resposta = new ResponseModel<List<Clientes>>();
            try
            {
                if (_context.Clientes.Count() == 0)
                {
                    resposta.Mensagem = "Não há clientes cadastrados!";
                    resposta.Status = false;
                    resposta.StatusCode = 404;
                    return resposta;
                }
                var clientes = await _context.Clientes.ToListAsync();
                resposta.Dados = clientes;
                resposta.Mensagem = "Todos os clientes foram coletados!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        public async Task<ResponseModel<Clientes>> GetClientes(int id)
        {
            ResponseModel<Clientes> resposta = new ResponseModel<Clientes>();
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

                if(cliente == null)
                {
                    resposta.Mensagem = "Cliente não encontrado!";
                    resposta.StatusCode = 404; 
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = cliente;
                resposta.Mensagem = "Cliente encontrado!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

        public async Task<ResponseModel<Clientes>> PostClientes(UserCreateDto UserCreateDto)
        {
            ResponseModel<Clientes> resposta = new ResponseModel<Clientes>();
            try
            {

                var cliente = new Clientes
                {
                    Nome = UserCreateDto.Nome,
                    Email = UserCreateDto.Email,
                    Numero = UserCreateDto.Numero,
                    DataNascimento = UserCreateDto.DataNascimento
                };

                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();
                resposta.Dados = cliente;
                resposta.Mensagem = "Cliente adicionado com sucesso!";
                resposta.StatusCode = 200;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }
       
        public async Task<ResponseModel<List<Clientes>>> PutClientes(int id, UserEditDto UserEditDto)
        {
            ResponseModel<List<Clientes>> resposta = new ResponseModel<List<Clientes>>();
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

                if (cliente == null){
                    resposta.Mensagem = "Cliente não encontrado!";
                    resposta.StatusCode = 404; 
                    return resposta;
                }

                cliente.Nome = UserEditDto.Nome;
                cliente.Email = UserEditDto.Email;
                cliente.Numero = UserEditDto.Numero;
                cliente.DataNascimento = UserEditDto.DataNascimento;

                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Clientes.ToListAsync();
                resposta.StatusCode = 200; 
                resposta.Mensagem = "Cliente atualizado com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500; 
                return resposta;
            }
        }

        public async Task<ResponseModel<List<Clientes>>> DeleteClientes(int id)
        {
            ResponseModel<List<Clientes>> resposta = new ResponseModel<List<Clientes>>();
            try
            {
                var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);

                if (cliente == null){
                    resposta.Mensagem = "Cliente não encontrado!";
                    resposta.StatusCode = 404;
                    return resposta;
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Clientes.ToListAsync();
                resposta.Mensagem = "Cliente deletado com sucesso!";
                resposta.StatusCode = 200;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                resposta.StatusCode = 500;
                return resposta;
            }
        }

    }
}