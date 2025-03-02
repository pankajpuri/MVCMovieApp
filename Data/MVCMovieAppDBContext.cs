using Microsoft.EntityFrameworkCore;
using MVCMovieApp.Models;
using System.Security.Principal;
namespace MVCMovieApp.Data
{
    public class MVCMovieAppDBContext:DbContext
    {
        public MVCMovieAppDBContext(DbContextOptions<MVCMovieAppDBContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Store> Store { get; set; } = default!;
        public DbSet<ProductSold> ProductSold { get; set; } = default!;
    }
}
