using System;
using System.Collections.Generic;

namespace ECommerceApi.Models
{
    public class ProductDataModel : ProductModel
    {
        public long Id { get; set; }
        public new List<ProductImageDataModel> Images { get; set; } = new List<ProductImageDataModel>();
    }

    public class ProductImageDataModel : ProductImageModel
    {
        public long Id { get; set; }
    }
}
