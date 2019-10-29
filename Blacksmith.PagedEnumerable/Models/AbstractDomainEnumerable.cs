using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blacksmith.PagedEnumerable.Exceptions;
using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.Validations;

namespace Blacksmith.PagedEnumerable.Models
{
    public abstract class AbstractDomainEnumerable<TIn, TOut, TOrder> : IDomainEnumerable<TOut, TOrder>
    {
        protected readonly Asserts assert;
        protected readonly IValidator validate;
        protected readonly IDomainEnumerableStrings strings;
        private readonly IQueryable<TIn> query;
        private readonly OrderStack<TOrder> orderStack;

        public AbstractDomainEnumerable(IQueryable<TIn> query, IDomainEnumerableStrings strings)
        {
            this.assert = Asserts.Assert;

            this.assert.isNotNull(query);
            this.assert.isNotNull(strings);

            this.strings = strings;
            this.validate = new Validator<PagedEnumerableException>(strings, prv_buildException);
            this.query = query;

            this.Page = new PageSettings(this.validate, this.strings);
            this.orderStack = new OrderStack<TOrder>(this.validate, this.strings);
        }

        public int TotalCount => this.query.Count();
        public IPageSettings Page { get; }
        public IOrderStack<TOrder> Order => this.orderStack;

        public IEnumerator<TOut> GetEnumerator()
        {
            return prv_enumerate();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return prv_enumerate();
        }

        private IEnumerator<TOut> prv_enumerate()
        {
            IQueryable<TIn> processedQuery;
            IEnumerable<TOut> finalQuery;

            processedQuery = prv_setOrderSettings(this.query, this.orderStack.Orders, prv_setFirstOrder, prv_setNextOrder, this.assert);
            processedQuery = prv_setPageSettings(processedQuery, this.Page);
            finalQuery = prv_setMappSettings(processedQuery, prv_mapToDomain, this.strings);

            return finalQuery.GetEnumerator();
        }

        private static IEnumerable<TOut> prv_setMappSettings(IQueryable<TIn> processedQuery, Func<TIn, TOut> prv_mapToDomain, IDomainEnumerableStrings strings)
        {
            try
            {
                return processedQuery
                    .AsEnumerable()
                    .Select(prv_mapToDomain);
            }
            catch (Exception ex)
            {
                throw new PagedEnumerableException(strings.Error_mapping_entity, ex);
            }
            
        }

        protected abstract IOrderedQueryable<TIn> prv_setFirstOrder(IQueryable<TIn> query, TOrder key, OrderDirection direction);
        protected abstract IOrderedQueryable<TIn> prv_setNextOrder(IOrderedQueryable<TIn> query, TOrder key, OrderDirection direction);
        protected abstract TOut prv_mapToDomain(TIn item);

        private static IQueryable<TIn> prv_setOrderSettings(
            IQueryable<TIn> query
            , IEnumerable<KeyValuePair<TOrder, OrderDirection>> orderConditions
            , Func<IQueryable<TIn>, TOrder, OrderDirection, IOrderedQueryable<TIn>> setFirstOrder
            , Func<IOrderedQueryable<TIn>, TOrder, OrderDirection, IOrderedQueryable<TIn>> setNextOrder
            , IValidator validate)
        {
            IList<KeyValuePair<TOrder, OrderDirection>> orders;

            orders = orderConditions.ToList();

            if (orders.Count > 0)
            {
                IOrderedQueryable<TIn> orderedQuery;
                KeyValuePair<TOrder, OrderDirection> orderCondition;

                orderCondition = orders[0];
                orders.RemoveAt(0);
                orderedQuery = setFirstOrder(query, orderCondition.Key, orderCondition.Value);
                validate.isNotNull(orderedQuery);

                while (orders.Count > 0)
                {
                    IOrderedQueryable<TIn> orderedSubQuery;

                    orderCondition = orders[0];
                    orders.RemoveAt(0);
                    orderedSubQuery = setNextOrder(orderedQuery, orderCondition.Key, orderCondition.Value);
                    validate.isNotNull(orderedSubQuery);
                    orderedQuery = orderedSubQuery;
                }

                return orderedQuery;
            }
            else
                return query;
        }

        private static IQueryable<TIn> prv_setPageSettings(IQueryable<TIn> query, IPageSettings pageSettings)
        {
            return query
                .Skip(pageSettings.Current * pageSettings.Size)
                .Take(pageSettings.Size);
        }

        private static PagedEnumerableException prv_buildException(string message)
        {
            return new PagedEnumerableException(message);
        }

    }
}
