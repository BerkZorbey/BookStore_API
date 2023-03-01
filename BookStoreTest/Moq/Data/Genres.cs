using BookStore_API.Models;
using BookStore_API.StoreDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreTest.Moq.Data
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange
                (
                    new Genre
                    {
                        Name = "Personel Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );
        }
    }
}
