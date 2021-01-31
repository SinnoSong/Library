namespace Library.API.Configs
{
    public class AuthorResourceParameters
    {
        public int PageNumber { get; set; } = 1;
        public const int MaxPageSize = 50;
        private int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        /// <summary>
        /// 过滤出生地属性
        /// </summary>
        public string BirthPlace { get; set; }

        /// <summary>
        /// 搜索属性
        /// </summary>
        public string SearchQuery { get; set; }
    }
}