using System.Net;
using Utilities;

namespace MediatR.Features.Abstractions;

public class CommandResult
{
    public static CommandResult Void() => new();

    public static CommandResult Created(string createdObjectUrl) => new(createdObjectUrl);

    public string CreatedObjectUrl { get; private set; }

    public HttpStatusCode StatusCode { get; private set; }

    CommandResult(string createdObjectUrl)
    {
        Ensure.IsNotNullOrEmpty(createdObjectUrl, nameof(createdObjectUrl));

        StatusCode = HttpStatusCode.Created;
        CreatedObjectUrl = createdObjectUrl;
    }

    CommandResult()
    {
        StatusCode = HttpStatusCode.NoContent;
    }
}
