using BookStore_API.BookStoreOperations.AuthorOperations.Commands.UpdateAuthor;
using BookStore_API.Models;
using BookStore_API.StoreDbContext;
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
    public class UpdateAuthorTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;

        public UpdateAuthorTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
        }

        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            UpdateAuthorCommand command = new(context);
            command.AuthorId = 999;

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No author found to be updated");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            // arrange
            UpdateAuthorCommand command = new(context);
            var author = new Author { Name = "Stefan", Surname = "Zweig", Birthday = new DateTime(1881, 11, 22) };

            context.Authors.Add(author);
            context.SaveChanges();

            command.AuthorId = author.Id;
            UpdateAuthorCommand.UpdateAuthorModel model = new UpdateAuthorCommand.UpdateAuthorModel { Name = "Gülseren", Surname = "Budayıcıoğlu", Birthday = new DateTime(1947, 11, 22) };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var updatedAuthor = context.Authors.SingleOrDefault(a => a.Id == author.Id);
            updatedAuthor.Should().NotBeNull();
            updatedAuthor.Name.Should().Be(model.Name);
            updatedAuthor.Surname.Should().Be(model.Surname);
            updatedAuthor.Birthday.Should().Be(model.Birthday);
        }
    }
}
