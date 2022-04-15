namespace Orders.Api.Helpers;

public class PaginatedList<T>
{
    public PaginatedList(int pageIndex, int pageSize, int dataCount, IReadOnlyList<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        DataCount = dataCount;
        Data = data;
    }

    public int PageIndex { get; }
    public int PageSize { get; }
    public int DataCount { get; }
    public IReadOnlyList<T> Data { get; }
}