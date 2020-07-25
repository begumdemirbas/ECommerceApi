using System;
using System.Collections.Generic;

namespace ECommerceApi.Models
{
    public class ProductUpdateInputModel : ProductModel
    {
        public long Id { get; set; }
        public new List<ProductImageInputModel> Images { get; set; } = new List<ProductImageInputModel>();
    }
}