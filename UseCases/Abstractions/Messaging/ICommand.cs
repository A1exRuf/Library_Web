using MediatR;

namespace UseCases.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}