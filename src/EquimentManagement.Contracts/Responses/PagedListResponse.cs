namespace EquimentManagement.Contracts.Responses;
public class PagedListResponse<T>
{
    public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();

    public int Page { get; init; }
    public int PageSize { get; init; }
    public bool IsLastPage { get; init; }
}
