using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR.Features.IntegrationTests.Fixtures;
using NUnit.Framework;

namespace MediatR.Features.IntegrationTests;

public class QueryTests
{
    HttpClient theClient;

    [SetUp]
    public void Setup()
    {
        var factory = new TheApi();
        theClient = factory.CreateClient();
    }

    [Test]
    public async Task does_not_exist()
    {
        var response = await theClient.GetAsync("/does-not-exist");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task get_request_with_query_parameters()
    {
        var response = await theClient.GetAsync("/widgets?count=1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();

        content.Should().NotContain("name2");
    }

    [Test]
    public async Task get_request_with_route_parameters()
    {
        var response = await theClient.GetAsync("/widgets/hello");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("hello1");
    }
}