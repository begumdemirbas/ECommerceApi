using System;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Data.EntityConfigurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage").HasKey(p => p.Id);

            builder.Property<string>("ImageUrl").IsRequired();
            builder.Property<long>("ProductId").IsRequired();
        }
    }
}
