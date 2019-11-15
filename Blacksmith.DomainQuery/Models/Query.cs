using System;
using System.Linq;

namespace Blacksmith.DomainQuery.Models
{
    public class Query<TIn, TOut> : AbstractQuery<TIn, TOut>
    {
        private readonly Func<TIn, TOut> mapToDomainDelegate;

        public Query(IQueryable<TIn> query, Func<TIn, TOut> mapToDomainDelegate) : base(query)
        {
            this.mapToDomainDelegate = mapToDomainDelegate 
                ?? throw new ArgumentNullException(nameof(mapToDomainDelegate));
        }

        protected override TOut mapToDomain(TIn item)
        {
            return this.mapToDomainDelegate(item);
        }
    }
}
