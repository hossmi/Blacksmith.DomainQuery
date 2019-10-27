using Blacksmith.Localized.Attributes;
using Blacksmith.Validations;

namespace Blacksmith.PagedEnumerable.Localization
{
    [Culture("en-US")]
    public class EnPagedEnumerableStrings : EnValidatorStrings, IPagedEnumerableStrings
    {
        public string Current_page_must_be_greater_or_equal_than_zero => "Current page must be greater or equal than zero";
        public string Page_size_must_be_greater_than_zero => "Page size must be greater than zero";
    }
}