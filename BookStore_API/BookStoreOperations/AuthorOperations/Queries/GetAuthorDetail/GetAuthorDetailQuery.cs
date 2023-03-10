using AutoMapper;
using BookStore_API.StoreDbContext;

namespace BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GetAuthorDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = context.Authors.SingleOrDefault(a => a.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("The Author not found");

            return mapper.Map<AuthorDetailViewModel>(author);
        }


        public class AuthorDetailViewModel
        {
            public string FullName { get; set; }
            public string Birthday { get; set; }
        }
    }
}
