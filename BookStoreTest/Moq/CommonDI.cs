using AutoMapper;
using BookStore_API.Mapper;
using BookStore_API.StoreDbContext;
using BookStoreTest.Moq.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreTest.Moq
{
    public class CommonDI
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonDI()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase("BookStoreTestDb").Options;
            Context = new(options);
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddAuthors();
            Context.AddGenres();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(config => { config.AddProfile<AutoMapperProfiles>(); }).CreateMapper();
        }
    }
}
