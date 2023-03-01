using BookStore_API.BookStoreOperations.AuthorOperations.Commands.UpdateAuthor;
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
    public class UpdateAuthorValidatorTest : IClassFixture<CommonDI>
    {
        private UpdateAuthorCommandValidator _validator;

        public UpdateAuthorValidatorTest()
        {
            _validator = new();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenAuthorIdIsInvalid_Validator_ShouldHaveError(int authorId)
        {
            // arrange
            var model = new UpdateAuthorCommand.UpdateAuthorModel { Name = "Stefan", Surname = "Zweig", Birthday = new DateTime(1881, 11, 22) };
            UpdateAuthorCommand command = new(null);
            command.Model = model;
            command.AuthorId = authorId;

            // act
            var result = _validator.Validate(command);

            // assert
            result.Errors.Should().ContainSingle();
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("ab", "cd")]
        [InlineData("12", "456")]
        [InlineData("123", "")]
        public void WhenModelIsInvalid_Validator_ShouldHaveError(string name, string surname)
        {
            // arrange
            var model = new UpdateAuthorCommand.UpdateAuthorModel { Name = name, Surname = surname, Birthday = new DateTime(2000, 11, 22) };
            UpdateAuthorCommand updateCommand = new(null);
            updateCommand.AuthorId = 3;
            updateCommand.Model = model;

            // act
            var result = _validator.Validate(updateCommand);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenVirthDayEqualNowGiven_Validator_ShouldBeReturnError()
        {
            // arrange
            UpdateAuthorCommand command = new(null);
            command.Model = new UpdateAuthorCommand.UpdateAuthorModel { Name = "George", Surname = "Orwell", Birthday = DateTime.Now.Date };

            // act
            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInputsAreValid_Validator_ShouldNotHaveError()
        {
            // arrange
            var model = new UpdateAuthorCommand.UpdateAuthorModel { Name = "Franz", Surname = "Kafka", Birthday = new DateTime(1883, 7, 3) };
            UpdateAuthorCommand updateCommand = new(null);
            updateCommand.AuthorId = 2;
            updateCommand.Model = model;

            // act
            var result = _validator.Validate(updateCommand);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
