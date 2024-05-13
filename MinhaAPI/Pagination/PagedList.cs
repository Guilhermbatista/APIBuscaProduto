namespace MinhaAPI.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int CurrentPage { get; set; }
    public int TotalPage { get; set;}
    public int PageSize { get; set;}
    public int TotalCount { get; set;}

    public bool HasPrevius => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalCount;


    public PagedList( List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPage = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public static PagedList<T> ToRagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count  = source.Count();
        var items = source.Skip((pageNumber -1) * pageSize).Take(pageSize).ToList();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
