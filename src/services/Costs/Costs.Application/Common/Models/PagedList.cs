namespace Costs.Application.Common.Models
{
    public class PagedList<TModel>
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public List<TModel> Items { get; set; } = new List<TModel>();
    }


    public class PagedListMetadata
    {
        private int _maxPageSize = 50;
        private int _normalPageSize = 5;
        private int _minPageSize = 1;


        private int _pageSize;
        public int PageSize
        {
            get
            {
                if (_pageSize > _maxPageSize)
                {
                    _pageSize = _maxPageSize;
                }
                else if (_pageSize == 0)
                {
                    _pageSize = _normalPageSize;
                }
                else if (_pageSize < _minPageSize)
                {
                    _pageSize = _minPageSize;
                }
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        private int _pageId;
        public int PageId
        {
            get
            {
                if (_pageId < 1)
                {
                    _pageId = 1;
                }
                return _pageId;
            }
            set
            {
                _pageId = value;
            }
        }
    }
}
