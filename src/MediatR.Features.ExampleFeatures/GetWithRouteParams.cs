using MediatR.Features.Abstractions.Attributes;
using MediatR.Features.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace MediatR.Features.ExampleFeatures;

public class GetWithRouteParams
{
    [HttpEndpoint("GET", "/widgets/{name}")]
    public class Query : QueryBase<IEnumerable<Model>>
    {
        public string Name { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Model>>
    {
        public Task<IEnumerable<Model>> Handle(
            Query request,
            CancellationToken cancellationToken)
        {
            IEnumerable<Model> result = Enumerable
                .Range(1, 5)
                .Select(x => new Model
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Name = request.Name + x
                });

            return Task.FromResult(result);
        }
    }

    public class Model
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
