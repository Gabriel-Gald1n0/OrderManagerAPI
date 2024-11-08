using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderManagerAPI.Models
{
    public class Produtos
    {
        [Key]
        public int Id { get; set; }

        public int TipoProdutoId { get; set; }

        public string? Nome { get; set; }
        
        public decimal Valor { get; set; }

        [JsonIgnore]
        public TipoProduto? TipoProduto { get; set; }
    }
}