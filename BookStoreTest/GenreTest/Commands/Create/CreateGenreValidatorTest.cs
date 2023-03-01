using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using Xunit;
using BookStoreTest.Moq;
using FluentAssertions;
using BookStore_API.BookStoreOperations.GenreOperations.Commands.CreateGenre;
using static BookStore_API.BookStoreOperations.GenreOperations.Commands.CreateGenre.CreateGenreCommand;

namespace BookStoreTest.GenreTest.Commands.Create
{
    public class CreateGenreValidatorTest : IClassFixture<CommonDI>
    {
        [Theory]
        [InlineData("a")]
        [InlineData("abc")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string name)
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel() { Name = name };

            CreateGenreCommandValidator validator = new();

            // act
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("abcd")]
        [InlineData("action")]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError(string name)
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel() { Name = name };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();

            // act
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
