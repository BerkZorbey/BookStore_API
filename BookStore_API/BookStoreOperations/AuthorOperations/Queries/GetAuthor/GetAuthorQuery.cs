using AutoMapper;
using BookStore_API.Models;
using BookStore_API.StoreDbContext;

namespace BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorQuery
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetAuthorQuery(BookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authorList = context.Authors.OrderBy(a => a.Id).ToList<Author>();
            return mapper.Map<List<AuthorsViewModel>>(authorList);
        }

        public class AuthorsViewModel
        {
            public string FullName { get; set; }
            public string Birthday { get; set; }
        }
    }
}
