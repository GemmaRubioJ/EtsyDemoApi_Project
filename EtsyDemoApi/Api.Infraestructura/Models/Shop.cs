using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Infraestructura.Models
{
    public class Shop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShopId { get; set; }
        public string? Name { get; set; } = "";
        public string ShopGuid { get; set; } = Guid.NewGuid().ToString();
        public string? Description { get; set; } = "";

        // Propiedad de navegación hacia los productos
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
