namespace Library.Common.Models;

public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalPages = (int) Math.Ceiling((decimal) totalCount / PageSize);
        AddRange(items);
    }

    public int CurrentPage { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public int PageSize { get; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var totalCount = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        var list = new PagedList<T>(items, totalCount, pageNumber, pageSize);
        return await Task.FromResult(list);
    }
}