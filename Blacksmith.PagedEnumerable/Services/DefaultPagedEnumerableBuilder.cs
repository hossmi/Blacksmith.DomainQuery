using Blacksmith.PagedEnumerable.Exceptions;
using Blacksmith.PagedEnumerable.Localization;
using Blacksmith.PagedEnumerable.Services;
using Blacksmith.Validations;

namespace Blacksmith.PagedEnumerable
{
    public class DefaultPagedEnumerableBuilder : IPagedEnumerableBuilder
    {
        private readonly IPagedEnumerableStrings strings;
        private readonly IValidator validator;

        public DefaultPagedEnumerableBuilder(IPagedEnumerableStrings strings)
        {
            this.strings = strings;
            this.validator = new Validator<PagedEnumerableException>(this.strings, prv_buildException);
        }

        private static PagedEnumerableException prv_buildException(string message)
        {
            return new PagedEnumerableException(message);
        }
    }
}