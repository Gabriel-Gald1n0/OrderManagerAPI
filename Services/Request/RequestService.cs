using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Models;
using WebAPI.Context;
using OrderManagerAPI.Dto.Request;

namespace OrderManagerAPI.Services.Request
{
    public class RequestService : RequestInterface
    {
        private readonly AppDbContext _context;

        public RequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<RequestResponseDto>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
            .Include(p => p.Cliente)
            .Include(p => p.Produto)
            .Include(p => p.StatusPedido)
            .Select(p => new RequestResponseDto
            {
                Id = p.Id,
                ClienteId = p.ClienteId,
                Cliente = p.Cliente != null ? p.Cliente.Nome : "Cliente não encontrado",
                ProdutoId = p.ProdutoId,
                Produto = p.Produto != null ? p.Produto.Nome : "Produto não encontrado",
                StatusPedidoId = p.StatusPedidoId,
                StatusPedido = p.StatusPedido != null ? p.StatusPedido.Status : "Status não encontrado",
                Quantidade = p.Quantidade
            })
            .ToListAsync();

            if (pedidos.Count == 0 || pedidos == null)
            {
                return new ResponseModel<List<RequestResponseDto>>
                {
                    Mensagem = "Não há pedidos cadastrados!",
                    Status = false,
                    StatusCode = 404
                };
            }

            return new ResponseModel<List<RequestResponseDto>>
            {
                Dados = pedidos,
                Mensagem = "Todos os pedidos foram coletados!",
                Status = true
            };
        }

        public async Task<ResponseModel<RequestResponseDto>> GetPedidos(int id)
        {
            var resposta = new ResponseModel<RequestResponseDto>();
            try
            {
                var pedido = await _context.Pedidos
                    .Include(x => x.Cliente)
                    .Include(x => x.Produto)
                    .Include(x => x.StatusPedido)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (pedido == null)
                {
                    resposta.Mensagem = "Pedido não encontrado!";
                    resposta.StatusCode = 404; 
                    resposta.Status = false;
                    return resposta;
                }

                resposta.Dados = new RequestResponseDto
                {
                    Id = pedido.Id,
                    ClienteId = pedido.ClienteId,
                    Cliente = pedido.Cliente?.Nome ?? "Cliente não encontrado",
                    ProdutoId = pedido.ProdutoId,
                    Produto = pedido.Produto?.Nome ?? "Produto não encontrado",
                    StatusPedidoId = pedido.StatusPedidoId,
                    StatusPedido = pedido.StatusPedido?.Status ?? "Status não encontrado",
                    Quantidade = pedido.Quantidade
                };

                resposta.Mensagem = "Pedido encontrado!";
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

        public async Task<ResponseModel<RequestResponseDto>> PostPedidos(RequestCreateDto RequestCreateDto)
        {
            ResponseModel<RequestResponseDto> resposta = new ResponseModel<RequestResponseDto>();
            try
            {
                Pedidos pedido = new Pedidos();
                pedido.ClienteId = RequestCreateDto.ClienteId;
                pedido.ProdutoId = RequestCreateDto.ProdutoId;
                pedido.StatusPedidoId = RequestCreateDto.StatusPedidoId;
                pedido.Quantidade = RequestCreateDto.Quantidade;
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == pedido.ClienteId);
                var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == pedido.ProdutoId);

                
                var pedidoResponse = new RequestResponseDto
                {
                    Id = pedido.Id,
                    ClienteId = pedido.ClienteId,
                    Cliente = cliente?.Nome ?? "Cliente não encontrado",
                    ProdutoId = pedido.ProdutoId,
                    Produto = produto?.Nome ?? "Produto não encontrado",
                    StatusPedidoId = pedido.StatusPedidoId,
                    Quantidade = pedido.Quantidade
                };

                resposta.Dados = pedidoResponse;
                resposta.Mensagem = "Pedido criado com sucesso!";
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

        public async Task<ResponseModel<List<Pedidos>>> PutPedidos(int id, RequestEditDto RequestEditDto)
        {
            ResponseModel<List<Pedidos>> resposta = new ResponseModel<List<Pedidos>>();
            try
            {
                var pedido = await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
                if(pedido == null)
                {
                    resposta.Mensagem = "Pedido não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }
                pedido.ClienteId = RequestEditDto.ClienteId;
                pedido.ProdutoId = RequestEditDto.ProdutoId;
                pedido.StatusPedidoId = RequestEditDto.StatusPedidoId;
                pedido.Quantidade = RequestEditDto.Quantidade;
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Pedidos.ToListAsync();
                resposta.Mensagem = "Pedido atualizado com sucesso!";
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

        public async Task<ResponseModel<List<RequestResponseDto>>> DeletePedidos(int id)
        {
            ResponseModel<List<RequestResponseDto>> resposta = new ResponseModel<List<RequestResponseDto>>();
            try
            {
                var pedido = await _context.Pedidos.FirstOrDefaultAsync(x => x.Id == id);
                if(pedido == null)
                {
                    resposta.Mensagem = "Pedido não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }


                _context.Pedidos.Remove(pedido);
                
                await _context.SaveChangesAsync();
                var pedidos = await _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Produto)
                    .Include(p => p.StatusPedido)
                    .Select(p => new RequestResponseDto
                    {
                        Id = p.Id,
                        ClienteId = p.ClienteId,
                        Cliente = p.Cliente != null ? p.Cliente.Nome : "Cliente não encontrado",
                        ProdutoId = p.ProdutoId,
                        Produto = p.Produto != null ? p.Produto.Nome : "Produto não encontrado",
                        StatusPedidoId = p.StatusPedidoId,
                        StatusPedido = p.StatusPedido != null ? p.StatusPedido.Status : "Status não encontrado",
                        Quantidade = p.Quantidade
                    })
                    .ToListAsync();

                resposta.Dados = pedidos;
                resposta.Mensagem = "Pedido deletado com sucesso!";
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