using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.DeleteAuthor;

internal sealed class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand, bool>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(
        IRepository<Author> authorRepository, 
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        bool result = await _authorRepository.RemoveByIdAsync(request.AuthorId, cancellationToken);
        
        if (!result)
            throw new AuthorNotFoundException(request.AuthorId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}