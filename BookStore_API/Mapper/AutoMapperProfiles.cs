using AutoMapper;
using BookStore_API.Models;
using BookStore_API.Models.DTOs;
using Movie_API.Models.Value_Object;
using static BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthor.GetAuthorQuery;
using static BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static BookStore_API.BookStoreOperations.BookOperations.Commands.CreateBook.CreateBookCommand;
using static BookStore_API.BookStoreOperations.BookOperations.Queries.GetBook.GetBooksQuery;
using static BookStore_API.BookStoreOperations.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenre.GetGenresQuery;
using static BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace BookStore_API.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();

            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name)).ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name + " " + src.Author.Surname));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
            CreateMap<Author, AuthorDetailViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
            CreateMap<CreateAuthorModel, Author>();

        }
    }
}
