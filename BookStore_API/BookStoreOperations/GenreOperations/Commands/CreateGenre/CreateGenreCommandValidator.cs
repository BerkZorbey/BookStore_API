using FluentValidation;

namespace BookStore_API.BookStoreOperations.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(4);
    }
}
}
