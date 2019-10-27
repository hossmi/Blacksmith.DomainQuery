using Blacksmith.Validations;

namespace Blacksmith.PagedEnumerable.Localization
{
    public interface IPagedEnumerableStrings : IValidatorStrings
    {
        string Current_page_must_be_greater_or_equal_than_zero { get; }
        string Page_size_must_be_greater_than_zero { get; }
    }
}