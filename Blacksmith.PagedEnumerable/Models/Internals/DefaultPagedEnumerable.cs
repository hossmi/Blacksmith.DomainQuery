using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.Validations;

namespace Blacksmith.PagedEnumerable.Models.Internals
{
    internal class DefaultPagedEnumerable<T> : IPagedEnumerable<T>
    {
        private readonly IPagedEnumerableStrings strings;
        private readonly IValidator validator;
        private readonly IQueryable<T> query;

        public DefaultPagedEnumerable(IQueryable<T> query, IValidator validator, IPagedEnumerableStrings strings)
        {
            this.strings = strings;
            this.validator = validator;
            this.query = query;
            this.Page = new PageSettings(this.validator, strings);
        }

        public int TotalCount => this.query.Count();
        public IPageSettings Page { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return prv_enumerate();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return prv_enumerate();
        }

        protected virtual IEnumerator<T> prv_enumerate()
        {
            throw new NotImplementedException();
        }

    }
}