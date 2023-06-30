using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MediatR.Features.AspNetCore.Features;

public sealed class NullFeature : FeatureBase
{
    public override void Map(IEndpointRouteBuilder builder)
    {
    }

    public override Task Execute(HttpContext context)
    {
        return Task.CompletedTask;
    }
}