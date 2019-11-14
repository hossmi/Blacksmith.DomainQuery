namespace Blacksmith.DomainQuery.Models
{
    public interface IPageSettings
    {
        int Current { get; set; }
        int Size { get; set; }
    }
}
