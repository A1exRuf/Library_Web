﻿using Core.Abstractions;
using Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Services;

sealed public class LinkService : ILinkService
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
    }

    public Link Generate(string endpointName, object? routeValues, string rel, string method)
    {
        return new Link(
            _linkGenerator.GetUriByName(
                _httpContextAccessor.HttpContext,
                endpointName,
                routeValues),
            rel,
            method);
    }
}
