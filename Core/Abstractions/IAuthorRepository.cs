using Core.Entities;

namespace Core.Abstractions;

public interface IAuthorRepository
{
    void Insert(Author author);
}

