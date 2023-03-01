using BookStore_API.BookStoreOperations.GenreOperations.Commands.DeleteGenre;
using BookStore_API.StoreDbContext;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.GenreTest.Commands.Delete
{
    public class DeleteGenreTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;

        public DeleteGenreTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
        }

        [Fact]
        public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            DeleteGenreCommand command = new(context);
            command.GenreId = 1400;

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("That Book Genre not found!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
        {
            // arrange
            DeleteGenreCommand command = new(context);
            command.GenreId = 1;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var genre = context.Genres.SingleOrDefault(g => g.Id == command.GenreId);
            genre.Should().BeNull();
        }
    }
}
