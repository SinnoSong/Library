namespace Library.Web.Models;

public class LendRecordQueryParameters : QueryParameters
{
    public string sort { get; set; }
    public Guid? userId { get; set; }
    public string? lendTime { get; set; }
    public string? returnTime { get; set; }
}