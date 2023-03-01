using AutoMapper;
using BookStore_API.BookStoreOperations.BookOperations.Commands.UpdateBook;
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
    public class UpdateBookTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public UpdateBookTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenGivenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)

            UpdateBookCommand command = new(context);
            command.BookId = 999;

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("No book found to be updated");

            // act (Çalıştırma)

            // assert (Doğrulama)
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // arrange
            UpdateBookCommand command = new(context);
            var book = new Book { Title = "Test Book", GenreId = 1, AuthorId = 1, PageCount = 100, PublishDate = new DateTime(2022, 1, 1) };

            context.Books.Add(book);
            context.SaveChanges();

            command.BookId = book.Id;
            UpdateBookCommand.UpdateBookModel model = new UpdateBookCommand.UpdateBookModel { Title = "Updated Title", GenreId = 2, AuthorId = 2 };
            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var updatedBook = context.Books.SingleOrDefault(b => b.Id == book.Id);
            updatedBook.Should().NotBeNull();
            updatedBook.PageCount.Should().Be(book.PageCount);
            updatedBook.PublishDate.Should().Be(book.PublishDate);
            updatedBook.Title.Should().Be(model.Title);
            updatedBook.GenreId.Should().Be(model.GenreId);
            updatedBook.AuthorId.Should().Be(model.AuthorId);
        }
    }
}
