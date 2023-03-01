using AutoMapper;
using BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor;
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

namespace BookStoreTest.AuthorTest.Commands.Create
{
    public class CreateAuthorTest : IClassFixture<CommonDI>
    { 
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public CreateAuthorTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenAlreadyExitsAuthorFullNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)
            var author = new Author()
            {
                Name = "Charlotte Perkins",
                Surname = "Gilman",
                Birthday = new DateTime(1860, 07, 03)
            };
            context.Authors.Add(author);
            context.SaveChanges();

            CreateAuthorCommand command = new(context, mapper);
            command.Model = new CreateAuthorCommand.CreateAuthorModel { Name = author.Name, Surname = author.Surname, Birthday = author.Birthday };

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Sequence contains more than one element");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // arrange
            CreateAuthorCommand command = new(context, mapper);
            CreateAuthorCommand.CreateAuthorModel model = new CreateAuthorCommand.CreateAuthorModel()
            {
                Name = "Taha Berk",
                Surname = "Yeşilalioğlu",
                Birthday = new DateTime(1999, 03, 28)
            };

            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var author = context.Authors.SingleOrDefault(g => g.Name == model.Name);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.Birthday.Should().Be(model.Birthday);
        }
    }
}
