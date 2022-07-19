namespace Library.Web.Models;

public class ApiException : Exception
{
    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception? innerException = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        Response = response;
        Headers = headers;
    }

    public int StatusCode { get; }
    public string Response { get; }
    public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }

    public override string ToString()
    {
        return $"HTTP Response: \n\n{Response}\n\n{base.ToString()}";
    }
}