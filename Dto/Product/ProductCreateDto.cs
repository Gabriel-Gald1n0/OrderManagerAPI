using System.ComponentModel.DataAnnotations;

namespace OrderManagerAPI.Dto.Product
{
    public class ProductCreateDto
    {
        public int TipoProdutoId { get; set; }
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do produto não pode ter mais de 100 caracteres.")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser um valor positivo.")]
        public decimal Valor { get; set; }
    }
}