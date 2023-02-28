using AutoMapper;
using BookStore_API.Models;
using BookStore_API.StoreDbContext;

namespace BookStore_API.BookStoreOperations.BookOperations.Commands.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var newBook = dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (newBook is not null)
                throw new InvalidOperationException("That Book already exists");

            newBook = mapper.Map<Book>(Model);
            dbContext.Books.Add(newBook);
            dbContext.SaveChanges();
        }

        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int AuthorId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
