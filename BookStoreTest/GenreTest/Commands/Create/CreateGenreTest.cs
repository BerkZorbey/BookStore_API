using AutoMapper;
using BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor;
using BookStore_API.BookStoreOperations.GenreOperations.Commands.CreateGenre;
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

namespace BookStoreTest.GenreTest.Commands.Create
{
    public class CreateGenreTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;

        public CreateGenreTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
        }

        [Fact]
        public void WhenAlreadyExitsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)
            var genre = new Genre()
            {
                Name = "Personel Growth",
                IsActive = true
            };
            context.Genres.Add(genre);
            context.SaveChanges();

            CreateGenreCommand command = new(context);
            command.Model = new CreateGenreCommand.CreateGenreModel { Name = genre.Name };

            // act & assert (Çalıştırma - Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Sequence contains more than one element");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            // arrange
            CreateGenreCommand command = new(context);
            CreateGenreCommand.CreateGenreModel model = new CreateGenreCommand.CreateGenreModel()
            {
                Name = "Test test",
            };

            command.Model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();
            // assert
            var genre = context.Genres.SingleOrDefault(g => g.Name == model.Name);
            genre.Should().NotBeNull();
            genre.IsActive.Should().BeTrue();
        }
    }

}
