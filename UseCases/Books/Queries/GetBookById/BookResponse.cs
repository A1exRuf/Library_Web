using Core.Entities;
using System;

namespace UseCases.Books.Queries.GetBookById;

public sealed record BookResponse(Guid Id, /*string ISBN,*/ string Title,
        string Genree, string Description, Author Author, DateTime TakenAt);
