using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderManagerAPI.Models
{
    public class TipoProduto
    {
        public int Id { get; set; }

        public string? Tipo { get; set; }

        [JsonIgnore]
        public ICollection<Produtos> Produtos { get; set; } = new List<Produtos>();
    }
}