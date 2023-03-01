using AutoMapper;
using BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenre;
using BookStore_API.StoreDbContext;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.GenreTest.Queries.Get
{
    public class GetGenreTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetGenreTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenGetGenresQueryIsHandled_GenreListShouldBeReturned()
        {
            // Arrange
            var query = new GetGenresQuery(context, mapper);

            // Act
            var result = query.Handle();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCountGreaterThan(0);

        }
    }
}
