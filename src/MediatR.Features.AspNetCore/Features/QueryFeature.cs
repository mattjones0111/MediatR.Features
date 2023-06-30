using System;
using System.Threading.Tasks;
using MediatR.Features.AspNetCore.Features.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Features.AspNetCore.Features;

public class QueryFeature : FeatureBase
{
    public QueryFeature(
        Type requestType,
        string routePattern)
        : base(requestType, "GET", routePattern)
    {
    }

    public override void Map(IEndpointRouteBuilder builder)
    {
        builder.MapMethods(
            RoutePattern,
            new[] { "GET" },
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

        IMediator mediator = context
            .RequestServices
            .GetRequiredService<IMediator>();

        var response = await mediator.Send(request);

        await context.Response.WriteAsJsonAsync(response);
    }
}