using System;
using System.Net;
using System.Threading.Tasks;
using MediatR.Features.Abstractions;
using MediatR.Features.AspNetCore.Features.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Utilities;

namespace MediatR.Features.AspNetCore.Features;

public sealed class CommandFeature : FeatureBase
{
    public static readonly string[] AllowedMethods = { "PUT", "POST", "DELETE" };

    public CommandFeature(
        Type requestType,
        string method,
        string routePattern)
        : base(requestType, method, routePattern)
    {
        Ensure.IsNotNull(requestType, nameof(requestType));
        Ensure.IsNotNull(method, nameof(method));
        Ensure.IsOneOf(method, AllowedMethods, nameof(method), StringComparer.OrdinalIgnoreCase);
    }

    public override void Map(IEndpointRouteBuilder builder)
    {
        builder.MapMethods(
            RoutePattern,
            new[] { Method },
            Execute);
    }

    public override async Task Execute(HttpContext context)
    {
        object request = Activator.CreateInstance(RequestType);

        if (request == null)
        {
            throw new RequestObjectConstructionException(RequestType);
        }

        BindRouteParameters(context.Request.RouteValues, request);
        BindQueryParameters(context.Request.Query, request);

        IServiceProvider provider = context.RequestServices;

        IMediator mediator = provider.GetService<IMediator>();

        var result = await mediator.Send(request);

        if (result is CommandResult commandResult)
        {
            context.Response.StatusCode = (int)commandResult.StatusCode;
        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.NoContent;
        }
    }
}
