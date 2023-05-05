using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TestCaseApp.Tests;

public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BasicTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Query_Profile_Without_Token_Should_Return_Unauthorized(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/UsersProfile");

        // Assert
        Assert.Equal(response.StatusCode, HttpStatusCode.Unauthorized); // Status Code 200-299
    }
}