using Core.Common;

namespace Core.Abstractions;

public interface ILinkService
{
    Link Generate(string endpointName, object? routeValues, string rel, string method);
}
