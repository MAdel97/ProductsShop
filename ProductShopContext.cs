namespace ProductsShop
{

    using global::ProductsShop.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using System.Reflection.Emit;

    namespace ProductsShop.EntityModel
    {
        public partial class ProductsShopContext : DbContext
        {
            public ProductsShopContext()
            {
            }

            public ProductsShopContext(DbContextOptions<ProductsShopContext> options)
                : base(options)
            {
            }

            public virtual DbSet<Product> products { get; set; }
            public virtual DbSet<User> users { get; set; }




            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer("Server=Hady-Sharawi\\SQLEXPRESS;Database=ProductsShop;Trusted_Connection=True;MultipleActiveResultSets=true");
                }
            }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<Product>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(entity => entity.Id).HasColumnName("Product_Id");


                    entity.ToTable("Products");

                });
                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasIndex(e => e.Name);
                    entity.HasIndex(e => e.Email);
                    entity.HasKey(e => e.UserId);


                    entity.ToTable("Users");

                });
                OnModelCreatingPartial(modelBuilder); 
            }


           
            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
    }

}
