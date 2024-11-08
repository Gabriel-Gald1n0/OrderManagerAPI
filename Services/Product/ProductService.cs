using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Models;
using WebAPI.Context;
using OrderManagerAPI.Dto.Product;

namespace OrderManagerAPI.Services.Product
{
    public class ProductService : ProductInterface
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<ProductResponseDto>>> GetProdutos()
        {
            ResponseModel<List<ProductResponseDto>> resposta = new ResponseModel<List<ProductResponseDto>>();
            try
            {
                if(await _context.Produtos.CountAsync() == 0)
                {
                    resposta.Mensagem = "Não há produtos cadastrados!";
                    resposta.Status = false;
                    resposta.StatusCode = 404; 
                    return resposta;
                }

                var produtos = await _context.Produtos
                                            .Include(p => p.TipoProduto)
                                            .Select(p => new ProductResponseDto
                                            {
                                                Id = p.Id,
                                                Nome = p.Nome,
                                                Valor = p.Valor,
                                                TipoProdutoId = p.TipoProdutoId,
                                                TipoProdutoNome = p.TipoProduto != null ? p.TipoProduto.Tipo : "Tipo não encontrado"
                                            })
                                            .ToListAsync();
                resposta.Dados = produtos;
                resposta.Mensagem = "Todos os produtos foram coletados!";
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

        public async Task<ResponseModel<ProductResponseDto>> GetProdutos(int id)
        {
            ResponseModel<ProductResponseDto> resposta = new ResponseModel<ProductResponseDto>();
            try
            {
                var produto = await _context.Produtos
                    .Include(p => p.TipoProduto)  // Inclui TipoProduto na consulta
                    .Where(x => x.Id == id)
                    .Select(p => new ProductResponseDto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Valor = p.Valor,
                        TipoProdutoId = p.TipoProdutoId,
                        TipoProdutoNome = p.TipoProduto != null ? p.TipoProduto.Tipo : "Tipo não encontrado"
                    })
                    .FirstOrDefaultAsync();
                if(produto == null)
                {
                    resposta.Mensagem = "Produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = produto;
                resposta.Mensagem = "Produto encontrado!";
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

        public async Task<ResponseModel<ProductResponseDto>> PostProdutos(ProductCreateDto ProductCreateDto)
        {
            ResponseModel<ProductResponseDto> resposta = new ResponseModel<ProductResponseDto>();
            try
            {
                Produtos produto = new Produtos();
                produto.Nome = ProductCreateDto.Nome;
                produto.Valor = ProductCreateDto.Valor;
                produto.TipoProdutoId = ProductCreateDto.TipoProdutoId;
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                var tipoProduto = await _context.TipoProdutos
                    .FirstOrDefaultAsync(tp => tp.Id == produto.TipoProdutoId);

                var produtoResponse = new ProductResponseDto
                {
                    Id = produto.Id,
                    TipoProdutoId = produto.TipoProdutoId,
                    TipoProdutoNome = tipoProduto?.Tipo ?? "Tipo não encontrado",
                    Nome = produto.Nome,
                    Valor = produto.Valor
                };


                resposta.Dados = produtoResponse;
                resposta.Mensagem = "Produto adicionado com sucesso!";
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

        public async Task<ResponseModel<List<Produtos>>> PutProdutos(int id, ProductEditDto ProductEditDto)
        {
            ResponseModel<List<Produtos>> resposta = new ResponseModel<List<Produtos>>();
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
                if(produto == null)
                {
                    resposta.Mensagem = "Produto não encontrado!";
                    resposta.StatusCode = 404;
                    resposta.Status = false;
                    return resposta;
                }
                produto.Nome = ProductEditDto.Nome;
                produto.Valor = ProductEditDto.Valor;
                produto.TipoProdutoId = ProductEditDto.TipoProdutoId;
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Produtos.ToListAsync();
                resposta.Mensagem = "Produto editado com sucesso!";
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

        public async Task<ResponseModel<List<ProductResponseDto>>> DeleteProdutos(int id)
        {
            ResponseModel<List<ProductResponseDto>> resposta = new ResponseModel<List<ProductResponseDto>>();
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);
                if(produto == null)
                {
                    resposta.Mensagem = "Produto não encontrado!";
                    resposta.Status = false;
                    resposta.StatusCode = 404;
                    return resposta;
                }
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                var produtos = await _context.Produtos
                                            .Include(p => p.TipoProduto)
                                            .Select(p => new ProductResponseDto
                                            {
                                                Id = p.Id,
                                                Nome = p.Nome,
                                                Valor = p.Valor,
                                                TipoProdutoId = p.TipoProdutoId,
                                                TipoProdutoNome = p.TipoProduto != null ? p.TipoProduto.Tipo : "Tipo não encontrado"
                                            })
                                            .ToListAsync();
                resposta.Dados = produtos;
                resposta.Mensagem = "Produto deletado com sucesso!";
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