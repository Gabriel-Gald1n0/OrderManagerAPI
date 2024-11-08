using OrderManagerAPI.Dto.Request;
using OrderManagerAPI.Models;

namespace OrderManagerAPI.Services.Request
{
    public interface RequestInterface
    {
        Task<ResponseModel<List<RequestResponseDto>>> GetPedidos();
        Task<ResponseModel<RequestResponseDto>> GetPedidos(int id);
        Task<ResponseModel<RequestResponseDto>> PostPedidos(RequestCreateDto RequestCreateDto);
        Task<ResponseModel<List<Pedidos>>> PutPedidos(int id, RequestEditDto RequestEditDto);
        Task<ResponseModel<List<RequestResponseDto>>> DeletePedidos(int id);
    }
}