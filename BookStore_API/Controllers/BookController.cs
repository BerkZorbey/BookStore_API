using AutoMapper;
using BookStore_API.BookStoreOperations.BookOperations.Commands.CreateBook;
using BookStore_API.BookStoreOperations.BookOperations.Commands.DeleteBook;
using BookStore_API.BookStoreOperations.BookOperations.Commands.UpdateBook;
using BookStore_API.BookStoreOperations.BookOperations.Queries.GetBook;
using BookStore_API.BookStoreOperations.BookOperations.Queries.GetBookDetail;
using BookStore_API.StoreDbContext;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(context, mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery.BookDetailViewModel result;

            GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookCommand.CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(context, mapper);
            command.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommand.UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();


            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
