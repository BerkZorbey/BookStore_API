using BookStore_API.BookStoreOperations.AuthorOperations.Commands.DeleteAuthor;
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
    public  class DeleteAuthorTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;

        public DeleteAuthorTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
        }

        [Fact]
        public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            DeleteAuthorCommand command = new(context);
            command.AuthorId = 120;

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author to be deleted was not found");
        }

        [Fact]
        public void WhenGivenAuthorHaveBook_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            DeleteAuthorCommand command = new(context);
            command.AuthorId = 1;

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author have book(s) so you can't delete");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
        {
            // arrange
            var newAuthor = new Author()
            {
                Name = "Yusuf",
                Surname = "Kızılkaya",
                Birthday = new DateTime(1994, 08, 07)
            };
            context.Authors.Add(newAuthor);
            context.SaveChanges();

            DeleteAuthorCommand command = new(context);
            command.AuthorId = newAuthor.Id;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var author = context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
            author.Should().BeNull();
        }
    }
}
