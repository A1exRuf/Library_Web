﻿namespace Core.Abstractions;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Role { get; }
}
