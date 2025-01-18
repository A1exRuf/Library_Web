using UseCases.Abstractions.Messaging;
using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;

namespace UseCases.Books.Commands.CreateBook;

internal sealed class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);

        if (author == null)
        {
            throw new AuthorNotFoundException(request.AuthorId);
        }

        var book = new Book(Guid.NewGuid(), request.Isbn, request.Title, request.Genree, request.Description, request.AuthorId, author);

        _bookRepository.Insert(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
