using AutoMapper;
using BookStore_API.Models;
using BookStore_API.StoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookStore_API.BookStoreOperations.BookOperations.Queries.GetBook
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = dbContext.Books.Include(x => x.Genre).Include(x => x.Author).OrderBy(b => b.Id).ToList<Book>();
            List<BooksViewModel> vm = mapper.Map<List<BooksViewModel>>(bookList);

            return vm;
        }

        public class BooksViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Author { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
