using FluentValidation;

namespace UseCases.Authors.Commands.DeleteAuthor
{
    public sealed class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty();
        }
    }
}
