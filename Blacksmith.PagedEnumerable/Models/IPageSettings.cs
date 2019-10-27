namespace Blacksmith.PagedEnumerable.Models
{
    public interface IPageSettings
    {
        int Current { get; set; }
        int Size { get; set; }
    }
}
