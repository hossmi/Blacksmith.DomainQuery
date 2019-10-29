using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.PagedEnumerable
{
    internal class OrderStack<TOrder> : IOrderStack<TOrder>
    {
        private readonly IValidator validate;
        private readonly IDomainEnumerableStrings strings;
        private readonly IList<KeyValuePair<TOrder,OrderDirection>> orders;

        public OrderStack(IValidator validate, IDomainEnumerableStrings strings)
        {
            this.validate = validate;
            this.strings = strings;
            this.orders = new List<KeyValuePair<TOrder, OrderDirection>>();
        }

        public int Count => this.orders.Count;

        public IEnumerable<KeyValuePair<TOrder, OrderDirection>> Orders => this.orders.ToList().AsReadOnly();

        public void clear()
        {
            this.validate.isTrue(this.orders.Count > 0, this.strings.There_are_no_order_conditions_for_removing);
            this.orders.Clear();
        }

        public void pop()
        {
            this.validate.isTrue(this.orders.Count > 0, this.strings.There_are_no_order_conditions_for_removing);
            this.orders.RemoveAt(this.orders.Count - 1);
        }

        public void push(TOrder key, OrderDirection direction)
        {
            throw new NotImplementedException("Check if condition was already added");
            this.orders.Add(new KeyValuePair<TOrder, OrderDirection>(key, direction));
        }
    }

}