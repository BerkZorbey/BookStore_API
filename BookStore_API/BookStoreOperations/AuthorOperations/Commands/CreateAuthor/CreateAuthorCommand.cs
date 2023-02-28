using AutoMapper;
using BookStore_API.Models;
using BookStore_API.Services;
using BookStore_API.StoreDbContext;
using MongoDB.Driver;

namespace BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand 
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;
        public CreateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void Handle()
        {
            var newAuthor = context.Authors.SingleOrDefault(a => a.Surname.ToUpper() == Model.Surname.ToUpper() && a.Name.ToUpper() == Model.Name.ToUpper());
            if (newAuthor is not null)
                throw new InvalidOperationException("That Author already exists");

            newAuthor = mapper.Map<Author>(Model);

            context.Authors.Add(newAuthor);
            context.SaveChanges();
        }

        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime Birthday { get; set; }
        }
    }
}
