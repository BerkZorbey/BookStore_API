using BookStore_API.BookStoreOperations.BookOperations.Queries.GetBookDetail;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.BookTest.Queries.GetDetail
{
    public class GetBookDetailValidatorTest : IClassFixture<CommonDI>
    {
        private GetBookDetailQueryValidator _validator;

        public GetBookDetailValidatorTest()
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
            GetBookDetailQuery query = new(null, null);
            query.BookId = bookId;

            // act
            var result = _validator.Validate(query);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenBookIdGreaterThanZero_ValidationShouldNotReturnError()
        {
            // arrange
            GetBookDetailQuery query = new(null, null);
            query.BookId = 12;

            // act
            var result = _validator.Validate(query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
