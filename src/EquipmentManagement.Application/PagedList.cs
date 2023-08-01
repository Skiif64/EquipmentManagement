using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Application;
public class PagedList<T>
{
    private readonly List<T> _items;
    public IEnumerable<T> Items => _items;

    public int Page { get; }
    public int PageSize { get; }
    public int PageCount { get; }
    public bool IsLastPage { get; }

    private PagedList(List<T> items, int page, int pageSize, int pageCount, bool isLastPage)
    {
        _items = items;
        Page = page;
        PageSize = pageSize;
        PageCount = pageCount;
        IsLastPage = isLastPage;
    }

    public static PagedList<T> Create(IQueryable<T> queryable, int page, int pageSize)
    {
        List<T> items;

        items = queryable
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();
                
        int pageCount = (int) Math.Ceiling((double)queryable.Count() / pageSize);
        bool isLastPage = page == pageCount;
        return new PagedList<T>(items, page, pageSize, pageCount, isLastPage);
    }

    public async static Task<PagedList<T>> CreateAsync(IQueryable<T> queryable, int page, int pageSize)
    {
        List<T> items;

        items = await queryable
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

        int pageCount = (int)Math.Ceiling((double)await queryable.CountAsync() / pageSize);
        bool isLastPage = page == pageCount;
        return new PagedList<T>(items, page, pageSize, pageCount, isLastPage);
    }
}
