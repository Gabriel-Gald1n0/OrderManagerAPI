namespace OrderManagerAPI.Dto.Product
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public int TipoProdutoId { get; set; }
        public string? TipoProdutoNome { get; set; }
        public string? Nome { get; set; }
        public decimal Valor { get; set; }
    }
}

