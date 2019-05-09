namespace react_netcore.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}