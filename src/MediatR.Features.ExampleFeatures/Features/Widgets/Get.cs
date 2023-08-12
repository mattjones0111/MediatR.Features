using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Features.Abstractions;
using MediatR.Features.Abstractions.Attributes;

namespace MediatR.Features.ExampleFeatures.Features.Widgets;

public class Get
{
    [HttpEndpoint("GET", "/widgets")]
    public class Query : QueryBase<IEnumerable<Model>>
    {
        public int Count { get; set; } = 5;
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Model>>
    {
        public Task<IEnumerable<Model>> Handle(
            Query request,
            CancellationToken cancellationToken)
        {
            IEnumerable<Model> result = Enumerable
                .Range(1, request.Count)
                .Select(x => new Model
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Name = "name" + x
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
