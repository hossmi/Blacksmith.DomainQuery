using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Models;
using Blacksmith.Validations;
using System;

namespace Blacksmith.PagedEnumerable
{
    internal class PageSettings : IPageSettings
    {
        private readonly IValidator validate;
        private readonly IPagedEnumerableStrings strings;
        private int current;
        private int size;

        public PageSettings(IValidator validator, IPagedEnumerableStrings strings)
        {
            this.validate = validator ?? throw new ArgumentNullException(nameof(validator));
            this.strings = strings ?? throw new ArgumentNullException(nameof(strings));
            this.current = 0;
            this.size = int.MaxValue;
        }

        public int Current
        {
            get => this.current;
            set
            {
                this.validate.isTrue(0 <= value, this.strings.Current_page_must_be_greater_or_equal_than_zero);
                this.current = value;
            }
        }
        public int Size
        {
            get => this.size;
            set
            {
                this.validate.isTrue(1 <= value, this.strings.Page_size_must_be_greater_than_zero);
                this.size = value;
            }
        }
    }
}