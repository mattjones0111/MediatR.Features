using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR.Features.ExampleContracts;
using MediatR.Features.IntegrationTests.Fixtures;
using NUnit.Framework;

namespace MediatR.Features.IntegrationTests;

public class CommandWithContractTests
{
    HttpClient theClient;

    [SetUp]
    public void Setup()
    {
        var factory = new TheApi();
        theClient = factory.CreateClient();
    }

    [Test]
    public async Task command_put_noninherited_contract()
    {
        var contract = new Person
        {
            FirstName = "Joe",
            LastName = "Bloggs",
            DateOfBirth = new DateTime(1977, 1, 1),
            Addresses = new[]
            {
                new PersonAddress
                {
                    Type = AddressType.Home,
                    Address = new Address
                    {
                        Line1 = "23 Railway Cuttings",
                        Town = "Cheam",
                        Country = "United Kingdom"
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(contract);

        var response = await theClient.PutAsync(
            "/people/1",
            new StringContent(json, Encoding.UTF8, "application/json"));

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
