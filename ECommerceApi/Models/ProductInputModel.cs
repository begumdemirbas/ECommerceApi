using System.Collections.Generic;

namespace ECommerceApi.Models
{
    public class ProductInputModel : ProductModel
    {
        public new List<ProductImageInputModel> Images { get; set; } = new List<ProductImageInputModel>();
    }

    public class ProductImageInputModel : ProductImageModel
    {
    }
}
