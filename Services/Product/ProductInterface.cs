using OrderManagerAPI.Dto.Product;
using OrderManagerAPI.Models;

namespace OrderManagerAPI.Services.Product
{
    public interface ProductInterface
    {
        Task<ResponseModel<List<ProductResponseDto>>> GetProdutos();
        Task<ResponseModel<ProductResponseDto>> GetProdutos(int id);
        Task<ResponseModel<ProductResponseDto>> PostProdutos(ProductCreateDto ProductCreateDto);
        Task<ResponseModel<List<Produtos>>> PutProdutos(int id, ProductEditDto ProductEditDto);
        Task<ResponseModel<List<ProductResponseDto>>> DeleteProdutos(int id);
    }
}