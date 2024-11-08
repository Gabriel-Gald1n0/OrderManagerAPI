using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OrderManagerAPI.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Numero { get; set; }

        public DateTime DataNascimento { get; set; }

        [JsonIgnore]
        public ICollection<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
    }
}
