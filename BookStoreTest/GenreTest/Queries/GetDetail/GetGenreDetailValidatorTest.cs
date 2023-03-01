using BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenreDetail;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.GenreTest.Queries.GetDetail
{
    public class GetGenreDetailValidatorTest : IClassFixture<CommonDI>
    {
        private GetGenreDetailQueryValidator _validator;

        public GetGenreDetailValidatorTest()
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
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = genreId;

            // act
            var result = _validator.Validate(query);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenGenreIdGreaterThanZero_ValidationShouldNotReturnError()
        {
            // arrange
            GetGenreDetailQuery query = new(null, null);
            query.GenreId = 12;

            // act
            var result = _validator.Validate(query);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
