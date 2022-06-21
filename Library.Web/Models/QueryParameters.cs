namespace Library.Web.Models
{
    public class QueryParameters
    {
        private int _pageSize = 25;
        public int StartIndex { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}
