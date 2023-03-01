using BookStore_API.BookStoreOperations.GenreOperations.Commands.DeleteGenre;
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
    public class DeleteGenreValidatorTest : IClassFixture<CommonDI>
    {
        private DeleteGenreCommandValidator _validator;

        public DeleteGenreValidatorTest()
        {
            _validator = new();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenGenreIdLessThanOrEqualZero_ValidationShouldReturnError(int genreId)
        {
            // arrange
            DeleteGenreCommand command = new(null);
            command.GenreId = genreId;

            // act
            var result = _validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
        {
            // arrange
            DeleteGenreCommand command = new(null);
            command.GenreId = 12;

            // act
            var result = _validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
