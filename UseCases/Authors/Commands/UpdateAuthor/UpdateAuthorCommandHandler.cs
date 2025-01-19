using UseCases.Abstractions.Messaging;
using Core.Abstractions;

namespace UseCases.Authors.Commands.UpdateAuthor;

internal sealed class UpdateAuthorCommandHandler : ICommandHandler<UpdateAuthorCommand, bool>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);

        if (author == null)
        {
            return false;
        }

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.DateOfBirth = request.DateOfBirth;
        author.Country = request.Country;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
