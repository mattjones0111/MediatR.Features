using NUnit.Framework;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace MediatR.Features.IntegrationTests;

public class CommandTests
{
    HttpClient theClient;

    [SetUp]
    public void Setup()
    {
        var factory = new TheApi();
        theClient = factory.CreateClient();
    }

    [Test]
    public async Task command_put()
    {
        var response = await theClient.PutAsync(
            "/widgets/1",
            new StringContent("{}", Encoding.UTF8, "application/json"));

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location
            .Should().NotBeNull()
            .And.Subject.Should().Be("http://localhost/widgets/1");
    }
}
