using System;
using System.ComponentModel.DataAnnotations;

namespace OrderManagerAPI.Dto.User
{
    public class ValidateDataNascimento
    {
        public static ValidationResult ValidateData(DateTime dataNascimento, ValidationContext context)
        {
            var idadeMinima = 15;
            var dataAtual = DateTime.Now;

            if (dataNascimento > dataAtual)
            {
                return new ValidationResult("A data de nascimento não pode ser no futuro.");
            }

            var idade = dataAtual.Year - dataNascimento.Year;
            if (dataNascimento.Date > dataAtual.AddYears(-idade)) idade--;

            if (idade < idadeMinima)
            {
                return new ValidationResult($"A idade mínima é {idadeMinima} anos.");
            }

            return ValidationResult.Success!;
        }
    }
}