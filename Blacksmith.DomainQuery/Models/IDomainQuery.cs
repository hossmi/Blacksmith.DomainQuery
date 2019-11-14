namespace Blacksmith.DomainQuery.Models
{
    public interface IDomainQuery<TOut, TOrder> : IPaginatedQuery<TOut>
    {
        IOrderStack<TOrder> Order { get; }
    }
}
