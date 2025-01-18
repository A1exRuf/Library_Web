using Core.Exceptions;
using System.Data;
using UseCases.Abstractions.Messaging;
using Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace UseCases.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IApplicationDbContext _context;

    public GetUserByIdQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<UserResponse> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context
            .Users
            .Where(u => u.Id == request.UserId)
            .Select(u => new UserResponse(
                u.Id,
                u.Name,
                u.Email,
                u.PasswordHash,
                u.Role))
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return user;
    }
}