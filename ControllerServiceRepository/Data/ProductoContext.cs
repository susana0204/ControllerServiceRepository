using ControllerServiceRepository.Entities;
using Microsoft.EntityFrameworkCore;
using ControllerServiceRepository.Models;

namespace ControllerServiceRepository.Data
{
    public class ControllerServiceRepositoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ControllerServiceRepositoryContext(DbContextOptions<ControllerServiceRepositoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Product producto1 = new Product()
            {
                Id = 1,
                Name = "Yerba mate",
                Price = 3500
            };

            modelBuilder.Entity<Product>().HasData(producto1);

            base.OnModelCreating(modelBuilder);
        }
    }
}