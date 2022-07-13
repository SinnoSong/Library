namespace Library.Web.Models
{
    public class ApiException : Exception
    {
        public int StatusCode { get; private set; }
        public string Response { get; private set; }
        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return $"HTTP Response: \n\n{Response}\n\n{base.ToString()}";
        }
    }
}