using AutoMapper;
using BookStore_API.BookStoreOperations.AuthorOperations.Commands.CreateAuthor;
using BookStore_API.BookStoreOperations.AuthorOperations.Commands.DeleteAuthor;
using BookStore_API.BookStoreOperations.AuthorOperations.Commands.UpdateAuthor;
using BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthor;
using BookStore_API.BookStoreOperations.AuthorOperations.Queries.GetAuthorDetail;
using BookStore_API.StoreDbContext;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public AuthorController(BookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAuthors()
        {
            GetAuthorQuery query = new(context, mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            GetAuthorDetailQuery query = new(context, mapper);
            query.AuthorId = id;
            GetAuthorDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult AddAuthor([FromBody] CreateAuthorCommand.CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new(context, mapper);
            command.Model = newAuthor;

            CreateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorCommand.UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new(context);
            command.AuthorId = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new(context);
            command.AuthorId = id;

            DeleteAuthorCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}
