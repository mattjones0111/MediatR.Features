using System;
using Utilities;

namespace MediatR.Features.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HttpEndpointAttribute : Attribute
{
    public string Method { get; }
    public string RoutePattern { get; }

    public HttpEndpointAttribute(string method, string routePattern)
    {
        Ensure.IsNotNullOrEmpty(method, nameof(method));
        Ensure.IsNotNullOrEmpty(routePattern, nameof(routePattern));

        Method = method;
        RoutePattern = routePattern;
    }
}
