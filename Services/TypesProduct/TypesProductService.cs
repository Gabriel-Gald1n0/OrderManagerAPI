using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Models;
using WebAPI.Context;
using OrderManagerAPI.Dto.TypesProduct;

namespace OrderManagerAPI.Services.TypesProduct
{
    public class TypesProductService : TypesProductInterface
    {
        private readonly AppDbContext _context;

        public TypesProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<TipoProduto>>> GetTipoProduto()
        {
            ResponseModel<List<TipoProduto>> resposta = new ResponseModel<List<TipoProduto>>();
            try
            {
                if(await _context.TipoProdutos.CountAsync() == 0)
                {
                    resposta.Mensagem = "Não há tipos de produtos cadastrados!";
                    resposta.Status = false;
                    resposta.StatusCode = 404;
                    return resposta;
                }
                var tipoProduto = await _context.TipoProdutos.ToListAsync();
                resposta.Dados = tipoProduto;
                resposta.Mensagem = "Todos os tipos de produtos foram coletados!";
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

        public async Task<ResponseModel<TipoProduto>> GetTipoProduto(int id)
        {
            ResponseModel<TipoProduto> resposta = new ResponseModel<TipoProduto>();
            try
            {
                var tipoProduto = await _context.TipoProdutos.FirstOrDefaultAsync(x => x.Id == id);
                if(tipoProduto == null)
                {
                    resposta.Mensagem = "Tipo de produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = tipoProduto;
                resposta.Mensagem = "Tipo de produto encontrado!";
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

        public async Task<ResponseModel<TipoProduto>> PostTipoProduto(TypesProductCreateDto TypesProductCreateDto)
        {
            ResponseModel<TipoProduto> resposta = new ResponseModel<TipoProduto>();
            try
            {
                TipoProduto tipoProduto = new TipoProduto();
                tipoProduto.Tipo = TypesProductCreateDto.Tipo;
                _context.TipoProdutos.Add(tipoProduto);
                await _context.SaveChangesAsync();
                resposta.Dados = tipoProduto;
                resposta.Mensagem = "Tipo de produto adicionado!";
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

        public async Task<ResponseModel<List<TipoProduto>>> PutTipoProduto(int id, TypesProductEditDto TypesProductEditDto)
        {
            ResponseModel<List<TipoProduto>> resposta = new ResponseModel<List<TipoProduto>>();
            try
            {
                var tipoProduto = await _context.TipoProdutos.FirstOrDefaultAsync(x => x.Id == id);
                if(tipoProduto == null)
                {
                    resposta.Mensagem = "Tipo de produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }
                tipoProduto.Tipo = TypesProductEditDto.Tipo;
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.TipoProdutos.ToListAsync();
                resposta.Mensagem = "Tipo de produto atualizado!";
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

        public async Task<ResponseModel<List<TipoProduto>>> DeleteTipoProduto(int id)
        {
            ResponseModel<List<TipoProduto>> resposta = new ResponseModel<List<TipoProduto>>();
            try
            {
                var tipoProduto = await _context.TipoProdutos.FirstOrDefaultAsync(x => x.Id == id);
                if(tipoProduto == null)
                {
                    resposta.Mensagem = "Tipo de produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }
                _context.TipoProdutos.Remove(tipoProduto);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.TipoProdutos.ToListAsync();
                resposta.Mensagem = "Tipo de produto deletado!";
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

            