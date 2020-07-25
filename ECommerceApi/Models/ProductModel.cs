using System.Collections.Generic;

namespace ECommerceApi.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<ProductImageModel> Images { get; set; } = new List<ProductImageModel>();
    }

    public class ProductImageModel
    {
        public string ImageUrl { get; set; }
    }
}
