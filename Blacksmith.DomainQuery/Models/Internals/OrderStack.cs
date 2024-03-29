﻿using Blacksmith.DomainQuery.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blacksmith.DomainQuery.Models.Internals
{
    internal class OrderStack<TOrder> : IOrderStack<TOrder>
    {
        private readonly IList<KeyValuePair<TOrder,OrderDirection>> orders;

        public OrderStack()
        {
            this.orders = new List<KeyValuePair<TOrder, OrderDirection>>();
        }

        public int Count => this.orders.Count;

        public void clear()
        {
            if (this.orders.Count <= 0)
                throw new EmptyOrderStackException();

            this.orders.Clear();
        }

        public void pop()
        {
            if(this.orders.Count <= 0)
                throw new EmptyOrderStackException();

            this.orders.RemoveAt(this.orders.Count - 1);
        }

        public void push(TOrder key, OrderDirection direction)
        {
            bool keyAlreadyAdded;

            keyAlreadyAdded = this.orders
                .Any(o => o.Key.Equals(key));

            if (keyAlreadyAdded)
                throw new AlreadyAddedOrderKeyException(key);

            this.orders.Add(new KeyValuePair<TOrder, OrderDirection>(key, direction));
        }

        public IEnumerator<KeyValuePair<TOrder, OrderDirection>> GetEnumerator()
        {
            return getEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return getEnumerator();
        }

        private IEnumerator<KeyValuePair<TOrder, OrderDirection>> getEnumerator()
        {
            return this.orders
                .Select(o => new KeyValuePair<TOrder, OrderDirection>(o.Key, o.Value))
                .GetEnumerator();
        }
    }

}