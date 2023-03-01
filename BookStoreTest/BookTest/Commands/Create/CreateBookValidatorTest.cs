using BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using Xunit;
using BookStoreTest.Moq;
using FluentAssertions;
using BookStore_API.BookStoreOperations.BookOperations.Commands.CreateBook;

namespace BookStoreTest.AuthorTest.Commands.Create
{
    public class CreateBookValidatorTest : IClassFixture<CommonDI>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1, 0)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 100, 1, 1)]
        [InlineData("", 0, 1, 1)]
        [InlineData("Lol", 100, 1, 1)]
        [InlineData("Lord", 100, 0, 1)]
        [InlineData("Lor", 100, 1, 0)]
        [InlineData(" ", 100, 1, 1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId, int authorId)
        {
            // arrange
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = genreId,
                AuthorId = authorId
            };

            // act
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                AuthorId = 1
            };

            // act
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            CreateBookCommand command = new(null, null);
            command.Model = new CreateBookCommand.CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                AuthorId = 1
            };

            // act
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
