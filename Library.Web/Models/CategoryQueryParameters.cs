namespace Library.Web.Models;

public class CategoryQueryParameters : QueryParameters
{
    public string Sort { get; set; } = "id";

    public string? Search { get; set; }
}