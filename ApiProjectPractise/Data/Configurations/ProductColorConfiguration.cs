using ApiProjectPractise.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiProjectPractise.Data.Configurations
{
    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.ColorId });

            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(pc => pc.Color)
                .WithMany(c => c.ProductColors)
                .HasForeignKey(pc => pc.ColorId);

            //seed data
            builder.HasData(
                new ProductColor { ProductId = 1, ColorId = 1 },
                new ProductColor { ProductId = 1, ColorId = 2 },
                new ProductColor { ProductId = 2, ColorId = 1 },
                new ProductColor { ProductId = 3, ColorId = 3 },
                new ProductColor { ProductId = 4, ColorId = 4 },
                new ProductColor { ProductId = 5, ColorId = 5 },
                new ProductColor { ProductId = 6, ColorId = 6 },
                new ProductColor { ProductId = 7, ColorId = 1 },
                new ProductColor { ProductId = 8, ColorId = 2 },
                new ProductColor { ProductId = 9, ColorId = 3 },
                new ProductColor { ProductId = 10, ColorId = 4 }


            );
        }

    }
}
