using ECommerceApi.DomainCore;

namespace ECommerceApi.Domain.AggregatesModel.ProductAggregate.Rules
{
    public class StockShouldBeGreaterOrEqualToZeroRule : IBusinessRule
    {
        private readonly int _stock;

        public StockShouldBeGreaterOrEqualToZeroRule(int stock)
        {
            _stock = stock;
        }

        public bool IsBroken()
        {
            return _stock < 0;
        }

        public string ExceptionResourceKey => "StockShouldBeGreaterOrEqualToZero";
    }
}