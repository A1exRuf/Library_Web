using Core.Exceptions.Base;

namespace UseCases.Exceptions
{
    internal class EmailExistsException : BadRequestException
    {
        public EmailExistsException() : base("Email has already been taken")
        { }
    }
}
