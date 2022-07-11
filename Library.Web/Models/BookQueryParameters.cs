namespace Library.Web.Models;

public class BookQueryParameters : QueryParameters
{
    public bool IsLend { get; set; }

    public string Sort { get; set; } = "title";

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Ibsn { get; set; }

    public string? Category { get; set; }
}