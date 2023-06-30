using System.Threading;
using System.Threading.Tasks;
using MediatR.Features.Abstractions;
using MediatR.Features.Abstractions.Attributes;

namespace MediatR.Features.ExampleFeatures;

public class Put
{
    [HttpEndpoint("PUT", "/widgets/{id}")]
    public class Command : CommandBase
    {
        public int Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, CommandResult>
    {
        public Task<CommandResult> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var result = CommandResult.Created($"widgets/{request.Id}");

            return Task.FromResult(result);
        }
    }
}
