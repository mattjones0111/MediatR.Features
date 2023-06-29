using System;
using Microsoft.AspNetCore.Routing;

namespace MediatR.Features.AspNetCore
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapFeatures(
            this IEndpointRouteBuilder builder,
            Type featureAssemblyMarkerType)
        {
            return builder;
        }
    }
}
