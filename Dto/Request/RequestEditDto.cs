using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace OrderManagerAPI.Dto.Request
{
    public class RequestEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ClienteId é obrigatório.")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "O ProdutoId é obrigatório.")]
        public int ProdutoId { get; set; }
        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        [DefaultValue(1)]
        public int StatusPedidoId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        [DefaultValue(1)]
        public int Quantidade { get; set; }
    }
}