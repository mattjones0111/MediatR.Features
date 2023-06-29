using System;

namespace MediatR.Features.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HttpEndpointAttribute : Attribute
{
    public string Method { get; }
    public string Route { get; }

    public HttpEndpointAttribute(string method, string route)
    {
        Method = method;
        Route = route;
    }
}
