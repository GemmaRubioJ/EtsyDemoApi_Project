using Api.Infraestructura.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
