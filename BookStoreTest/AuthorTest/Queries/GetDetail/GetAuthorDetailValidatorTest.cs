using BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthorDetail;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.AuthorTest.Queries.GetDetail 
{
    public class GetAuthorDetailValidatorTest : IClassFixture<CommonDI>
    {
        private GetAuthorDetailQueryValidator _validator;

        public GetAuthorDetailValidatorTest()
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
            GetAuthorDetailQuery query = new(null, null);
            query.AuthorId = authorId;

            // act
            var result = _validator.Validate(query);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenAuthorIdGreaterThanZero_ValidationShouldNotReturnError()
        {
            // arrange
            GetAuthorDetailQuery query = new(null, null);
            query.AuthorId = 12;

            // act
            var result = _validator.Validate(query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
