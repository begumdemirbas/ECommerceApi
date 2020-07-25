using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceApi.Domain.AggregatesModel.ProductAggregate.Rules;
using ECommerceApi.DomainCore;

namespace ECommerceApi.Domain.AggregatesModel.ProductAggregate
{
    public class Product : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Barcode { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        private List<ProductImage> _images { get; } = new List<ProductImage>();
        public IReadOnlyCollection<ProductImage> Images => _images;

        public Product()
        {
        }

        public Product(string name, string description, string barcode, decimal price, int stock, List<ProductImage> images)
        {
            CheckRule(new StockShouldBeGreaterOrEqualToZeroRule(stock));
            Name = name;
            Description = description;
            Barcode = barcode;
            Price = price;
            Stock = stock;
            _images.AddRange(images);
        }

        public void Update(string name, string description, string barcode, decimal price, int stock, List<ProductImage> images)
        {
            CheckRule(new StockShouldBeGreaterOrEqualToZeroRule(stock));
            Name = name;
            Description = description;
            Barcode = barcode;
            Price = price;
            Stock = stock;

            var deletedImages = Images.Select(x => x.Id).Except(images.Select(x => x.Id)).ToList();
            foreach (var deletedId in deletedImages)
            {
                Images.FirstOrDefault(x => x.Id == deletedId).Delete();
            }

            var addedImages = images.Where(p => p.Id == 0).ToList();
            _images.AddRange(addedImages);
        }

        public void Delete()
        {
            IsDeleted = true;
            foreach (var image in _images)
            {
                image.Delete();
            }
        }

        private void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new ApplicationException(rule.ExceptionResourceKey);
            }
        }
    }
}
