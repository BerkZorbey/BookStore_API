using BookStore_API.BookStoreOperations.BookOperations.Commands.DeleteBook;
using BookStore_API.StoreDbContext;
using BookStoreTest.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookStoreTest.BookTest.Commands.Delete
{
    public class DeleteBookTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;

        public DeleteBookTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
        }

        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            DeleteBookCommand command = new(context);
            command.BookId = 1100;

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book to be deleted was not found");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
        {
            // arrange
            DeleteBookCommand command = new(context);
            command.BookId = 1;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var book = context.Books.SingleOrDefault(b => b.Id == command.BookId);
            book.Should().BeNull();
        }
    }
}
