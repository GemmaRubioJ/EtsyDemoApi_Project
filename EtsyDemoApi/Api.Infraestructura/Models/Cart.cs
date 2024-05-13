using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Infraestructura.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        // Lista de productos en el carrito
        public virtual List<CartItem> Products { get; set; }

        // Versión de seguimiento, opcional
        public int Version { get; set; }
    }

    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        [JsonIgnore]
        public virtual Cart Cart { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
