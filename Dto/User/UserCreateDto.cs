using System.ComponentModel.DataAnnotations;
using static OrderManagerAPI.Dto.User.ValidateDataNascimento;

namespace OrderManagerAPI.Dto.User
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string? Nome { get; set; }
        
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Número de telefone inválido")]
        [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "O número de telefone deve estar no formato (XX) XXXXX-XXXX")]
        public string? Numero { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(ValidateDataNascimento), nameof(ValidateDataNascimento.ValidateData))]
        public DateTime DataNascimento { get; set; }
    }
}