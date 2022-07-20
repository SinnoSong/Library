namespace Library.Web.Models;

public class UserQueryParameters : QueryParameters
{
    public string Grade { get; set; } = "1";
    public bool IsAdmin { get; set; }
}