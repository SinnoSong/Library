namespace Library.Web.Models;

public class UpdateUserGradeDto
{
    public Guid UserId { get; set; }
    public string Grade { get; set; } = "1";
}