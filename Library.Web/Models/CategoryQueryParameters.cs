namespace Library.Web.Models;

public class CategoryQueryParameters : QueryParameters
{
    public string sort { get; set; }
    public string? search { get; set; }
}