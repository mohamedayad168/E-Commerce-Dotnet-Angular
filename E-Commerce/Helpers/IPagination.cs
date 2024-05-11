
namespace E_Commerce.Helpers
{
    public interface IPagination<T> where T : class
    {
        int Count { get; set; }
        IEnumerable<T> Data { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}