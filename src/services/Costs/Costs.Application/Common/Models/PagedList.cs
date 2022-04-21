namespace Costs.Application.Common.Models
{
    public class PagedList<TModel>
    {
        public int TotalCount { get; set; }


        private int _pageSize;
        public int PageSize
        {
            get
            {
                if (_pageSize < 1)
                {
                    throw new ArgumentException("مقدار 'تعداد در هر صفحه' نمی تواند کمتر از '1' باشد");
                }

                return _pageSize;
            }
            set 
            { 
                
                _pageSize = value;
            }
        }


        private int _currentPage;
        public int CurrentPage
        {
            get
            {
                if (_currentPage < 1)
                {
                    throw new ArgumentException("مقدار 'شماره صفحه' نمی تواند کمتر از '1' باشد");
                }

                if (_currentPage > TotalPages)
                {
                    throw new ArgumentException("مقدار 'شماره صفحه' نمی تواند بیش تر از 'تعداد صفحات' باشد");
                }

                return _currentPage;
            }
            set {

                
                _currentPage = value;
            }
        }


        public int TotalPages => (int)Math.Ceiling((decimal)TotalCount / PageSize);

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public List<TModel> Items { get; set; } = new List<TModel>();
    }
}
