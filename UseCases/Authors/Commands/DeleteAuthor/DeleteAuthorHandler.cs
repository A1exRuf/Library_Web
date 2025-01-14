using Core.Abstractions;
using UseCases.Abstractions.Messaging;
using UseCases.Authors.Commands.DeleteAuthor;

internal sealed class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand, bool>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);

        if (author == null)
        {
            return false;
        }

        _authorRepository.Delete(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}