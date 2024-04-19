using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Request
{
    public class CreateProductRequest
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        [Required] public int ShopId { get; set; }
    }
}
