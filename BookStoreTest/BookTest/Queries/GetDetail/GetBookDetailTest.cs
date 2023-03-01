using AutoMapper;
using BookStore_API.BookStoreOperations.BookOperations.Queries.GetBookDetail;
using BookStore_API.StoreDbContext;
using BookStoreTest.Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookStore_API.BookStoreOperations.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;

namespace BookStoreTest.BookTest.Queries.GetDetail
{
    public class GetBookDetailTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetBookDetailTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
        {
            // arrange
            GetBookDetailQuery query = new(context, mapper);
            var BookId = query.BookId = 1;

            var book = context.Books.Include(x => x.Genre).Include(x => x.Author).Where(b => b.Id == BookId).SingleOrDefault();

            // act
            BookDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.Title.Should().Be(book.Title);
            vm.PageCount.Should().Be(book.PageCount);
            vm.Genre.Should().Be(book.Genre.Name);
            vm.Author.Should().Be(book.Author.Name + " " + book.Author.Surname);
            vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));
        }

        [Fact]
        public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            int bookId = 1300;

            GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
            query.BookId = bookId;

            // assert
            query.Invoking(x => x.Handle())
                 .Should().Throw<InvalidOperationException>()
                 .And.Message.Should().Be("The Book Not Found");
        }
    }
}
