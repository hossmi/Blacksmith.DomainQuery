using Blacksmith.Localized.Attributes;
using Blacksmith.Validations;

namespace Blacksmith.PagedEnumerable.Localization
{
    [Culture("en-US")]
    public class EnDomainEnumerableStrings : EnValidatorStrings, IDomainEnumerableStrings
    {
        public string Current_page_must_be_greater_or_equal_than_zero => "Current page must be greater or equal than zero";
        public string Page_size_must_be_greater_than_zero => "Page size must be greater than zero";

        public string There_are_no_order_conditions_for_removing => "There are no order conditions for removing";
        public string Error_mapping_entity => "Error mapping entity";
    }
}