using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace OrderManagerAPI.Models
{
    public class Pedidos
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public int StatusPedidoId { get; set; }
        public int Quantidade { get; set; }
        
        [JsonIgnore]
        public StatusPedido? StatusPedido { get; set; }
        [JsonIgnore]
        public Clientes? Cliente { get; set; }
        [JsonIgnore]
        public Produtos? Produto { get; set; }
    }
}