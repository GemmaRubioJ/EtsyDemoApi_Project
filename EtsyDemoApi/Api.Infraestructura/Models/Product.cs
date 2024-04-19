using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Infraestructura.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; } 
        public string ProductGuid { get; set; } =  Guid.NewGuid().ToString(); //valor númerico (Global Unique Identifier) aunque se guarda en string
                                                                               // Clave foránea de Shop
        public int ShopId { get; set; }

        // Propiedad de navegación hacia Shop
        [ForeignKey("ShopId")]
        [JsonIgnore]
        public Shop? Shop { get; set; }
    }
}
