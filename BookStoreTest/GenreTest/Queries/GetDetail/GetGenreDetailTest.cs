using AutoMapper;
using BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenreDetail;
using BookStore_API.StoreDbContext;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace BookStoreTest.GenreTest.Queries.GetDetail
{
    public class GetGenreDetailTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetGenreDetailTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeReturned()
        {
            // arrange
            GetGenreDetailQuery query = new(context, mapper);
            var GenreId = query.GenreId = 1;

            var genre = context.Genres.Where(g => g.Id == GenreId).SingleOrDefault();

            // act
            GenreDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.Name.Should().Be(genre.Name);
        }

        [Fact]
        public void WhenNonExistingGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            int genreId = 1700;

            GetGenreDetailQuery query = new(context, mapper);
            query.GenreId = genreId;

            // assert
            query.Invoking(x => x.Handle())
                 .Should().Throw<InvalidOperationException>()
                 .And.Message.Should().Be("Book genre not found");
        }
    }
}
