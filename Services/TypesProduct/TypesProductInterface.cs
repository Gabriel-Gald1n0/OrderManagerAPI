using OrderManagerAPI.Dto.TypesProduct;
using OrderManagerAPI.Models;

namespace OrderManagerAPI.Services.TypesProduct
{
    public interface TypesProductInterface
    {
        Task<ResponseModel<List<TipoProduto>>> GetTipoProduto();
        Task<ResponseModel<TipoProduto>> GetTipoProduto(int id);
        Task<ResponseModel<TipoProduto>> PostTipoProduto(TypesProductCreateDto TypesProductCreateDto);
        Task<ResponseModel<List<TipoProduto>>> PutTipoProduto(int id, TypesProductEditDto TypesProductEditDto);
        Task<ResponseModel<List<TipoProduto>>> DeleteTipoProduto(int id);
    }
}