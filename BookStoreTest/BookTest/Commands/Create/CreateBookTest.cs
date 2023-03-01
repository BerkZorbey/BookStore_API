using AutoMapper;
using BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor;
using BookStore_API.BookStoreOperations.BookOperations.Commands.CreateBook;
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
using static BookStore_API.BookStoreOperations.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStoreTest.AuthorTest.Commands.Create
{
    public class CreateBookTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public CreateBookTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenAlreadyExitsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)
            var book = new Book()
            {
                Title = "WhenAlreadyExitsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10),
                GenreId = 1,
                AuthorId = 1
            };
            context.Books.Add(book);
            context.SaveChanges();

            CreateBookCommand command = new(context, mapper);
            command.Model = new CreateBookCommand.CreateBookModel() { Title = book.Title };

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("That Book already exists");

            // act (Çalıştırma)

            // assert (Doğrulama)
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            // arrange
            CreateBookCommand command = new(context, mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-11),
                GenreId = 1,
                AuthorId = 3
            };

            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var book = context.Books.SingleOrDefault(b => b.Title == model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
            book.AuthorId.Should().Be(model.AuthorId);
        }
    }
    
}
