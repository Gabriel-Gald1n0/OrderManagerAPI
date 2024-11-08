namespace OrderManagerAPI.Dto.Request
{
    public class RequestResponseDto
    {
        public int Id { get; set; }
        
        public int ClienteId { get; set; }
        public string? Cliente { get; set; }
        
        public int ProdutoId { get; set; }
        public string? Produto { get; set; } 
        
        public int StatusPedidoId { get; set; }
        public string? StatusPedido { get; set; }
        public int Quantidade { get; set; }
    }
}