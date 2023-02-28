using BookStore_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore_API.StoreDbContext
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
