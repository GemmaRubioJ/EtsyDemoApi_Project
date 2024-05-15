using Api.Infraestructura.Models;
using Microsoft.EntityFrameworkCore;


namespace Api.Infraestructura.Context
{
    public class ApiContext :DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


    }
}
