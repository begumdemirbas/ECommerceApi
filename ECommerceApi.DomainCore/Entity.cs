namespace ECommerceApi.DomainCore
{

    public abstract class Entity : BaseEntity<long>
    {

    }

    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
