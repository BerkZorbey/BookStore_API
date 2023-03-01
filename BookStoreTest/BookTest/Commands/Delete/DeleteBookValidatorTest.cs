using BookStore_API.BookStoreOperations.BookOperations.Commands.DeleteBook;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.AuthorTest.Commands.Delete
{
    public class DeleteBookValidatorTest : IClassFixture<CommonDI>
    {
        private DeleteBookCommandValidator _validator;

        public DeleteBookValidatorTest()
        {
            _validator = new();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenBookIdLessThanOrEqualZero_ValidationShouldReturnError(int bookId)
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            // act
            var result = _validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenBookIdGreaterThanZero_ValidationShouldNotReturnError()
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 12;

            // act
            var result = _validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
