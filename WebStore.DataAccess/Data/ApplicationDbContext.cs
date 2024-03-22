using Microsoft.EntityFrameworkCore;
using WebStore.Models;

namespace WebStore.DataAccess.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Product>? Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>().HasNoKey();
        }

    }
}
