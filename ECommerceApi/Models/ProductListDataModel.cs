using System.Collections.Generic;

namespace ECommerceApi.Models
{
    public class ProductListDataModel
    {
        public List<ProductDataModel> Data { get; set; }
        public string Message { get; set; }
        public int DataCount { get; set; }
    }
}
