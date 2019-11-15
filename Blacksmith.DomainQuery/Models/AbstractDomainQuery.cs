using Blacksmith.DomainQuery.Exceptions;
using Blacksmith.DomainQuery.Models.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blacksmith.DomainQuery.Models
{
    public abstract class AbstractDomainQuery<TIn, TOut, TOrder> : AbstractQuery<TIn, TOut>, IDomainQuery<TOut, TOrder>
    {
        private readonly OrderStack<TOrder> orderStack;

        public AbstractDomainQuery(IQueryable<TIn> query) : base(query)
        {
            this.orderStack = new OrderStack<TOrder>();
        }

        public IOrderStack<TOrder> Order => this.orderStack;

        protected abstract Expression<Func<TIn, object>> getKeySelector(TOrder key);

        protected override IQueryable<TIn> processQuery(IQueryable<TIn> query)
        {
            IList<KeyValuePair<TOrder, OrderDirection>> orders;

            orders = this.orderStack.Orders;

            if (orders.Count > 0)
            {
                IOrderedQueryable<TIn> orderedQuery;
                KeyValuePair<TOrder, OrderDirection> orderCondition;
                Expression<Func<TIn, object>> keySelector;

                orderCondition = orders[0];
                orders.RemoveAt(0);

                keySelector = getKeySelector(orderCondition.Key) 
                    ?? throw new NullOrderedQueryException(orderCondition.Key);

                orderedQuery = query.orderBy(keySelector, orderCondition.Value);

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
                return query;
        }
    }
}
