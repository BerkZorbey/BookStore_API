using AutoMapper;
using BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthorDetail;
using BookStore_API.StoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using Xunit;
using BookStoreTest.Moq;
using FluentAssertions;

namespace BookStoreTest.AuthorTest.Queries.GetDetail
{
    public class GetAuthorDetailTest : IClassFixture<CommonDI>
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetAuthorDetailTest(CommonDI commonDI)
        {
            this.context = commonDI.Context;
            this.mapper = commonDI.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeReturned()
        {
            // arrange
            GetAuthorDetailQuery query = new(context, mapper);
            var AuthorId = query.AuthorId = 1;

            var author = context.Authors.Where(a => a.Id == AuthorId).SingleOrDefault();

            // act
            AuthorDetailViewModel vm = query.Handle();

            // assert
            vm.Should().NotBeNull();
            vm.FullName.Should().Be(author.Name + " " + author.Surname);
            vm.Birthday.Should().Be(author.Birthday.ToString());
        }

        [Fact]
        public void WhenNonExistingAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            int authorId = 1000;

            GetAuthorDetailQuery query = new(context, mapper);
            query.AuthorId = authorId;

            // assert
            query.Invoking(x => x.Handle())
                 .Should().Throw<InvalidOperationException>()
                 .And.Message.Should().Be("The Author not found");
        }
    }
}
