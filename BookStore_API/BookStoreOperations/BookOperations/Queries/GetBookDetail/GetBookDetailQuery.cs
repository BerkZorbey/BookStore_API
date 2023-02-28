using AutoMapper;
using BookStore_API.StoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookStore_API.BookStoreOperations.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper mapper;
        public int BookId { get; set; }

        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = dbContext.Books.Include(x => x.Genre).Include(x => x.Author).Where(b => b.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new InvalidOperationException("The Book Not Found");
            BookDetailViewModel vm = mapper.Map<BookDetailViewModel>(book); 
            return vm;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public string Author { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
