using Costs.Application.Common.Models;
using System;
using Xunit;

namespace Costs.Application.Test.Common.Models
{
    public class PagedListTest
    {

        #region PageSize

        [Trait("Common", "Pagination")]
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void When_PageSize_LowerThan_1_Return_Throw_ArgumentException<TModel>(int pageSize)
        {
            PagedList<TModel> pagedList = GetDefaultPagedListItem<TModel>(pageSize: pageSize);

            var result = Record.Exception(() => pagedList.PageSize);

            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }


        [Trait("Common", "Pagination")]
        [Theory]
        [InlineData(2)]
        [InlineData(100)]
        public void When_PageSize_GreaterThan_0_Return_DosentThrow_Exception<TModel>(int pageSize)
        {
            PagedList<TModel> pagedList = GetDefaultPagedListItem<TModel>(pageSize: pageSize);

            var result = Record.Exception(() => pagedList.PageSize);

            Assert.Null(result);
        }

        #endregion


        #region CurrentPage

        [Trait("Common", "Pagination")]
        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void When_CurrentPage_LowerThan_1_Returen_ArgumentException<TModel>(int currentPage)
        {
            PagedList<TModel> pagedList = GetDefaultPagedListItem<TModel>(currentPage: currentPage);

            var result = Record.Exception(() => pagedList.CurrentPage);

            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }


        [Trait("Common", "Pagination")]
        [Theory]
        [InlineData(12)]
        [InlineData(45)]
        public void When_CurrentPage_GreaterThan_0_Return_DosentThrow_Exception<TModel>(int currentPage)
        {
            PagedList<TModel> pagedList = new PagedList<TModel>()
            {
                CurrentPage = currentPage,
                PageSize = 5,
                TotalCount = int.MaxValue
            };

            var result = Record.Exception(() => pagedList.CurrentPage);

            Assert.Null(result);
        }


        [Theory]
        [Trait("Common", "Pagination")]
        [InlineData(2, 5, 5)]
        [InlineData(5, 5, 10)]
        [InlineData(1, 0, 10)]
        public void When_CurrentPage_GreaterThan_TotalPages_Return_ArgumentException<TModel>(int currentPage, int totalCount, int pageSize)
        {
            PagedList<TModel> pagedList = new PagedList<TModel>()
            {
                CurrentPage = currentPage,
                TotalCount = totalCount,
                PageSize = pageSize
            };

            var result = Record.Exception(() => pagedList.CurrentPage);

            Assert.NotNull(result);
            Assert.IsType<ArgumentException>(result);
        }

        #endregion


        #region HasPrevious

        [Trait("Common", "Pagination")]
        [Theory]
        [InlineData(2)]
        [InlineData(13)]
        public void When_CurrentPage_GreaterThan_0_HasPrevious_Equal_True<TModel>(int currentPage)
        {
            PagedList<TModel> pagedList = GetDefaultPagedListItem<TModel>(currentPage: currentPage);

            Assert.Equal(true, pagedList.HasPrevious);
        }

       
        #endregion


        #region HasNext

        [Trait("Common", "Pagination")]// CurrentPage < TotalPages = ((int)Math.Ceiling((decimal)TotalCount / PageSize))
        [Theory]
        [InlineData(1, 5, 6)]
        [InlineData(5, 5, 26)]
        [InlineData(25, 2, 60)]
        [InlineData(3, 10, 32)]
        public void When_CurrentPage_LowerThan_TotalPages_HasNext_Equal_True<TModel>(int currentPage, int pageSize, int totalCount)
        {
            PagedList<TModel> pagedList = GetDefaultPagedListItem<TModel>(currentPage, pageSize, totalCount);

            Assert.Equal(true, pagedList.HasNext);
        }


        [Trait("Common", "Pagination")]// CurrentPage < TotalPages = ((int)Math.Ceiling((decimal)TotalCount / PageSize))
        [Theory]
        [InlineData(1, 5, 3)]
        [InlineData(5, 2, 10)]
        [InlineData(8, 10, 80)]
        public void When_CurrentPage_GreaterOrEqualThan_TotalPages_HasNext_Equal_False<TModel>(int currentPage, int pageSize, int totalCount)
        {
            PagedList<TModel> pagedList = GetDefaultPagedListItem<TModel>(currentPage, pageSize, totalCount);

            Assert.Equal(false, pagedList.HasNext);
        }

        #endregion


        private PagedList<TModel> GetDefaultPagedListItem<TModel>(int currentPage = 1, int pageSize = 1, int totalCount = int.MaxValue)
        {
            PagedList<TModel> pagedList = new PagedList<TModel>()
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return pagedList;
        }

    }
}
