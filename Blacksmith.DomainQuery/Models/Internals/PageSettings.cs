using System;

namespace Blacksmith.DomainQuery.Models.Internals
{
    internal class PageSettings : IPageSettings
    {
        private int current;
        private int size;

        public PageSettings()
        {
            this.current = 0;
            this.size = int.MaxValue;
        }

        public int Current
        {
            get => this.current;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Current page must be greater or equal than zero.", nameof(this.Current));

                this.current = value;
            }
        }
        public int Size
        {
            get => this.size;
            set
            {
                if (value < 1)
                    throw new ArgumentException($"Page size must be greater than zero.", nameof(this.Size));

                this.size = value;
            }
        }
    }

}