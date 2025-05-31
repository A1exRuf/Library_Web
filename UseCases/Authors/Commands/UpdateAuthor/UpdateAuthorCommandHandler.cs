using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Core.Filters;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.UpdateAuthor;

internal sealed class UpdateAuthorCommandHandler : ICommandHandler<UpdateAuthorCommand, bool>
{
    private readonly IRepository<Author> _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorCommandHandler(IRepository<Author> authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetAsync(
            new AuthorFilter { Id = request.AuthorId },
            asNoTracking: false,
            cancellationToken) ?? throw new AuthorNotFoundException(request.AuthorId);

        author.FirstName = request.FirstName ?? author.FirstName;
        author.LastName = request.LastName ?? author.LastName;
        author.DateOfBirth = request.DateOfBirth ?? author.DateOfBirth;
        author.Country = request.Country ?? author.Country;

        _authorRepository.Update(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
