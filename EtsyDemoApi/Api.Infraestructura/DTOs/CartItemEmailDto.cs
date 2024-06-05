namespace Api.Infraestructura.DTOs
{
    public class CartItemEmailDto
    {
        public int ProductId { get; set; }
        public string Title{ get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
