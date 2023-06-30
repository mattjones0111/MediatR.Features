using System;
using System.Collections.Generic;
using System.Linq;
using MediatR.Features.AspNetCore.Features;
using Microsoft.AspNetCore.Routing;
using Utilities;

namespace MediatR.Features.AspNetCore;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapFeatures(
        this IEndpointRouteBuilder builder,
        Type featureAssemblyMarkerType)
    {
        Ensure.IsNotNull(featureAssemblyMarkerType, nameof(featureAssemblyMarkerType));

        IEnumerable<FeatureBase> features = featureAssemblyMarkerType
            .Assembly
            .GetExportedTypes()
            .Where(FeatureBase.IsFeature)
            .Select(FeatureBase.CreateFromType);

        foreach (var feature in features)
        {
            feature.Map(builder);
        }

        return builder;
    }
}