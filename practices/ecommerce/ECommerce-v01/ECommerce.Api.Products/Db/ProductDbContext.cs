using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Db
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
