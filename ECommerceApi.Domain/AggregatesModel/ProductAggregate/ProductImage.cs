using ECommerceApi.DomainCore;

namespace ECommerceApi.Domain.AggregatesModel.ProductAggregate
{
    public class ProductImage : Entity
    {
        public string ImageUrl { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public ProductImage()
        {
        }

        public ProductImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
