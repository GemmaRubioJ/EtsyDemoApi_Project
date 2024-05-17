namespace Api.Infraestructura.DTOs
{
    public class EmailDataDto
    {
        public string Email { get; set; }
        public List<CartItemEmailDto> CartItems { get; set; }
    }
}
