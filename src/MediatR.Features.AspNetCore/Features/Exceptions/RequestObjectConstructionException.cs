using System;

namespace MediatR.Features.AspNetCore.Features.Exceptions;

public class RequestObjectConstructionException : Exception
{
    public Type Type { get; }

    public RequestObjectConstructionException(Type type)
        : base($"Error constructing instance of {type.Name}.")
    {
        Type = type;
    }
}
