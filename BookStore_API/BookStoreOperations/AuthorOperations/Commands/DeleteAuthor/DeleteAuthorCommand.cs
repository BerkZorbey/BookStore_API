using BookStore_API.StoreDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookStore_API.BookStoreOperations.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext context;

        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            this.context = context;
        }

        public void Handle()
        {
            var author = context.Authors.Include(a => a.Books).SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("The author to be deleted was not found");
            if (author.Books.Any())
                throw new InvalidOperationException("Author have book(s) so you can't delete");

            context.Authors.Remove(author);
            context.SaveChanges();
        }
    }
}
