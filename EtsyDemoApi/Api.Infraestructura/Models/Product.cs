using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Api.Infraestructura.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int ProductId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; } = "";
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; } = "";
        [JsonProperty("description")]
        public string Description { get; set; } = "";
        [JsonProperty("image")]
        public string Image { get; set; } = "";
        [JsonProperty("rating")]
        public ProductRating Rating { get; set; } = new ProductRating();
        public string ProductGuid { get; set; } =  Guid.NewGuid().ToString(); //valor númerico (Global Unique Identifier) aunque se guarda en string
                                                                          

    
    }
    public class ProductRating
    {
        [JsonProperty("rate")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public double Rate { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
