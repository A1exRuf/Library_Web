using UseCases.Abstractions.Messaging;

namespace UseCases.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
