using Blacksmith.DomainQuery.Exceptions;
using Blacksmith.DomainQuery.Models.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blacksmith.DomainQuery.Models
{
    public abstract class AbstractDomainQuery<TIn, TOut, TOrder> : IDomainQuery<TOut, TOrder>
    {
        private readonly IQueryable<TIn> query;
        private readonly OrderStack<TOrder> orderStack;

        public AbstractDomainQuery(IQueryable<TIn> query)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
            this.Page = new PageSettings();
            this.orderStack = new OrderStack<TOrder>();
        }

        public int TotalCount => this.query.Count();
        public IPageSettings Page { get; }
        public IOrderStack<TOrder> Order => this.orderStack;

        public IEnumerator<TOut> GetEnumerator()
        {
            return enumerate();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return enumerate();
        }

        private IEnumerator<TOut> enumerate()
        {
            return getOrderedQuery()
                    .paginate(this.Page.Size, this.Page.Current)
                    .AsEnumerable()
                    .Select(mapToDomain)
                    .GetEnumerator();
        }

        protected abstract Expression<Func<TIn, object>> getKeySelector(TOrder key);
        protected abstract TOut mapToDomain(TIn item);

        private IQueryable<TIn> getOrderedQuery()
        {
            IList<KeyValuePair<TOrder, OrderDirection>> orders;

            orders = this.orderStack.Orders.ToList();

            if (orders.Count > 0)
            {
                IOrderedQueryable<TIn> orderedQuery;
                KeyValuePair<TOrder, OrderDirection> orderCondition;
                Expression<Func<TIn, object>> keySelector;

                orderCondition = orders[0];
                orders.RemoveAt(0);

                keySelector = getKeySelector(orderCondition.Key) 
                    ?? throw new NullOrderedQueryException(orderCondition.Key);

                orderedQuery = this.query.orderBy(keySelector, orderCondition.Value);

                while (orders.Count > 0)
                {
                    orderCondition = orders[0];
                    orders.RemoveAt(0);

                    keySelector = getKeySelector(orderCondition.Key)
                        ?? throw new NullOrderedQueryException(orderCondition.Key);

                    orderedQuery = orderedQuery.thenBy(keySelector, orderCondition.Value);
                }

                return orderedQuery;
            }
            else
                return this.query;
        }
    }
}
