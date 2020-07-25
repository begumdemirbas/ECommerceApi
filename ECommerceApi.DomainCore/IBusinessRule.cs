using System;
namespace ECommerceApi.DomainCore
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string ExceptionResourceKey { get; }
    }
}
