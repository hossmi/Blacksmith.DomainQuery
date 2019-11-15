namespace Blacksmith.DomainQuery.Models
{
    public interface IDomainQuery<TOut, TOrder> : IQuery<TOut>
    {
        IOrderStack<TOrder> Order { get; }
    }
}
