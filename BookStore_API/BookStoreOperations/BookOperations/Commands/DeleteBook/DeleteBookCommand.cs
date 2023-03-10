using BookStore_API.StoreDbContext;

namespace BookStore_API.BookStoreOperations.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext dbContext;
        public int BookId { get; set; }

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("The book to be deleted was not found");

            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
        }
    }
}
