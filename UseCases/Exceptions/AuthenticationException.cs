using Core.Exceptions.Base;

namespace UseCases.Exceptions;

internal class AuthenticationException : BadRequestException
{
    public AuthenticationException() : base("User must be authenticated")
    { }
}
