using Core.Abstractions;
using Core.Entities;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.CreateAuthor;

internal sealed class CreateAuthorCommandHandler : ICommandHandler<CreateAuthorCommand, Guid>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorCommandHandler(
        IRepository<Author> authorRepository, 
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        Author author = new(
            Guid.NewGuid(), 
            request.FirstName, 
            request.LastName,
            request.DateOfBirth, 
            request.Country);

        await _authorRepository.AddAsync(author, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}
