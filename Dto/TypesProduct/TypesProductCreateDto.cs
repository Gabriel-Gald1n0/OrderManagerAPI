using System.ComponentModel.DataAnnotations;

namespace OrderManagerAPI.Dto.TypesProduct
{
    public class TypesProductCreateDto
    {
        [StringLength(50, ErrorMessage = "O tipo do produto não pode ter mais de 50 caracteres.")]
        public string? Tipo { get; set; }
    }
}