using Blacksmith.DomainQuery.Exceptions;
using Blacksmith.DomainQuery.Models.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        protected abstract IOrderedQueryable<TIn> setFirstOrder(IQueryable<TIn> query, TOrder key, OrderDirection direction);
        protected abstract IOrderedQueryable<TIn> setNextOrder(IOrderedQueryable<TIn> query, TOrder key, OrderDirection direction);
        protected abstract TOut mapToDomain(TIn item);

        private IQueryable<TIn> getOrderedQuery()
        {
            IList<KeyValuePair<TOrder, OrderDirection>> orders;

            orders = this.orderStack.Orders.ToList();

            if (orders.Count > 0)
            {
                IOrderedQueryable<TIn> orderedQuery;
                KeyValuePair<TOrder, OrderDirection> orderCondition;

                orderCondition = orders[0];
                orders.RemoveAt(0);

                orderedQuery = setFirstOrder(this.query, orderCondition.Key, orderCondition.Value)
                    ?? throw new NullOrderedQueryException(orderCondition.Key);

                while (orders.Count > 0)
                {
                    IOrderedQueryable<TIn> orderedSubQuery;

                    orderCondition = orders[0];
                    orders.RemoveAt(0);

                    orderedSubQuery = setNextOrder(orderedQuery, orderCondition.Key, orderCondition.Value)
                        ?? throw new NullOrderedQueryException(orderCondition.Key);

                    orderedQuery = orderedSubQuery;
                }

                return orderedQuery;
            }
            else
                return this.query;
        }
    }
}
