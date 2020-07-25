using ECommerceApi.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").HasKey(p => p.Id);

            builder.Property<string>("UserName").IsRequired();
            builder.Property<string>("Password").IsRequired();
            builder.Property<bool>("IsAdmin").IsRequired();
        }
    }
}
