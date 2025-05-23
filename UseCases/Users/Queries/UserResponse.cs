﻿using Core.Common;

namespace UseCases.Users.Queries;

public sealed record UserResponse(
    Guid Id,
    string Name,
    string Email,
    string PasswordHash,
    string Role,
    List<Link> Links);
