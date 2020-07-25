using System;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(p => p.Id);

            builder.Property<string>("Name").IsRequired();
            builder.Property<string>("Description").IsRequired();
            builder.Property<string>("Barcode").IsRequired();
            builder.Property<decimal>("Price").IsRequired();
            builder.Property<int>("Stock").IsRequired();

            builder.HasMany(x => x.Images)
                .WithOne(x => x.Product)
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
