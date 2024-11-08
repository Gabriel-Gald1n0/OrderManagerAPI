using OrderManagerAPI.Dto.User;
using OrderManagerAPI.Models;

namespace OrderManagerAPI.Services.User
{
    public interface UserInterface
    {
        Task<ResponseModel<List<Clientes>>> GetClientes();
        Task<ResponseModel<Clientes>> GetClientes(int id);
        Task<ResponseModel<Clientes>> PostClientes(UserCreateDto UserCreateDto);
        Task<ResponseModel<List<Clientes>>> PutClientes(int id, UserEditDto UserEditDto);
        Task<ResponseModel<List<Clientes>>> DeleteClientes(int id);
    }
}
