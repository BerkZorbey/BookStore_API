using AutoMapper;
using BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthor;
using BookStore_API.StoreDbContext;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.AuthorTest.Queries.Get
{
    public class GetAuthorTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetAuthorTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenGetAuthorsQueryIsHandled_AuthorListShouldBeReturned()
        {
            // Arrange
            var query = new GetAuthorQuery(context, mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);

        }
    }
}
