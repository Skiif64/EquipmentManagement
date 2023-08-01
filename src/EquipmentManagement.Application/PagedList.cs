using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application;
public class PagedList<T>
{
    private readonly List<T> _items;
    public IEnumerable<T> Items => _items;

    public int Page { get; }
    public int PageSize { get; }
    public bool IsLastPage { get; }

    private PagedList(List<T> items, int page, int pageSize, bool isLastPage)
    {
        _items = items;
        Page = page;
        PageSize = pageSize;
        IsLastPage = isLastPage;
    }

    public static PagedList<T> Create(IQueryable<T> queryable, int page, int pageSize)
    {
        List<T> items;

        items = queryable
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

        bool isLastPage = items.Count < pageSize;
        return new PagedList<T>(items, page, pageSize, isLastPage);
    }

    public async static Task<PagedList<T>> CreateAsync(IQueryable<T> queryable, int page, int pageSize)
    {
        List<T> items;

        items = await queryable
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        bool isLastPage = items.Count < pageSize;
        return new PagedList<T>(items, page, pageSize, isLastPage);
    }
}
