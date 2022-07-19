namespace Library.Web.Models;

public class UserQueryParameters : QueryParameters
{
    public byte? Grade { get; set; }
    public bool? IsAdmin { get; set; }
}