﻿using Core.Abstractions;
using Core.Entities;
using UseCases.Abstractions.Messaging;

namespace UseCases.Authors.Commands.CreateAuthor;

internal sealed class CreateAuthorCommandHandler : ICommandHandler<CreateAuthorCommand, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author(Guid.NewGuid(), request.FirstName, request.LastName,
            request.DateOfBirth, request.Country);

        _authorRepository.Add(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}
