using AutoMapper;
using BookStore_API.BookStoreOperations.GenreOperations.Commands.CreateGenre;
using BookStore_API.BookStoreOperations.GenreOperations.Commands.DeleteGenre;
using BookStore_API.BookStoreOperations.GenreOperations.Commands.UpdateGenre;
using BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenre;
using BookStore_API.BookStoreOperations.GenreOperations.Queries.GetGenreDetail;
using BookStore_API.StoreDbContext;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BookStore_API.BookStoreOperations.GenreOperations.Commands.CreateGenre.CreateGenreCommand;
using static BookStore_API.BookStoreOperations.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : BaseController
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new(context, mapper);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpGet("{id}")]
        public ActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new(context, mapper);
            query.GenreId = id;
            GetGenreDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new(context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updatedGenre)
        {
            UpdateGenreCommand command = new(context);
            command.GenreId = id;
            command.Model = updatedGenre;

            UpdateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new(context);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}
