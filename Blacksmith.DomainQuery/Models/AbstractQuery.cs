using Blacksmith.DomainQuery.Exceptions;
using Blacksmith.DomainQuery.Models.Internals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.DomainQuery.Models
{
    public abstract class AbstractQuery<TIn, TOut> : IQuery<TOut>
    {
        private readonly IQueryable<TIn> query;

        public AbstractQuery(IQueryable<TIn> query)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
            this.Page = new PageSettings();
        }

        public int TotalCount => this.query.Count();

        public IPageSettings Page { get; }

        public IEnumerator<TOut> GetEnumerator()
        {
            return getEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return getEnumerator();
        }

        private IEnumerator<TOut> getEnumerator()
        {
            IQueryable<TIn> processedQuery;

            processedQuery = processQuery(this.query) 
                ?? throw new NullProcessedQueryException();

            return processedQuery
                    .paginate(this.Page.Size, this.Page.Current)
                    .AsEnumerable()
                    .Select(mapToDomain)
                    .GetEnumerator();
        }

        protected virtual IQueryable<TIn> processQuery(IQueryable<TIn> query)
        {
            return query;
        }

        protected abstract TOut mapToDomain(TIn item);
    }
}
