namespace Library.Web.Models;

public class BookQueryParameters : QueryParameters
{
    public bool isLend { get; set; }
    public string? sort { get; set; }
    public string? title { get; set; }
    public string? author { get; set; }
    public string? ibsn { get; set; }
    public Guid? category { get; set; }
}