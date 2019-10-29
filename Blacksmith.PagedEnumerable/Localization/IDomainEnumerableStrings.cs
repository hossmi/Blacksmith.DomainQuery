using Blacksmith.Validations;

namespace Blacksmith.PagedEnumerable.Localization
{
    public interface IDomainEnumerableStrings : IValidatorStrings
    {
        string Current_page_must_be_greater_or_equal_than_zero { get; }
        string Page_size_must_be_greater_than_zero { get; }
        string There_are_no_order_conditions_for_removing { get; }
        string Error_mapping_entity { get; }
    }
}