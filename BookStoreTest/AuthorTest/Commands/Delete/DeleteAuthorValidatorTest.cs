using BookStore_API.BookStoreOperations.AuthorOperations.Commands.DeleteAuthor;
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
    internal class DeleteAuthorValidatorTest : IClassFixture<CommonDI>
    {
        private DeleteAuthorCommandValidator _validator;

        public DeleteAuthorValidatorTest()
        {
            _validator = new();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenAuthorIdLessThanOrEqualZero_ValidationShouldReturnError(int authorId)
        {
            // arrange
            DeleteAuthorCommand command = new(null);
            command.AuthorId = authorId;

            // act
            var result = _validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        public void WhenAuthorIdGreaterThanZero_ValidationShouldNotReturnError(int authorId)
        {
            // arrange
            DeleteAuthorCommand command = new(null);
            command.AuthorId = authorId;

            // act
            var result = _validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
