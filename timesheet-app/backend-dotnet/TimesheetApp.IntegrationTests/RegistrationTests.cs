using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.IntegrationTests;

public class RegistrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public RegistrationTests(WebApplicationFactory<Program> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Get_RegistrationsInDatabase_ReturnsRegistrations()
    {
        // Arrange

        // Act
        var result = await _httpClient.GetAsync("api/registrations");

        // Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        var registrations = await result.Content.ReadFromJsonAsync<List<Registration>>();
        registrations.ShouldNotBeNull();
    }

    [Fact]
    public async Task Get_RegistrationsInDatabaseFrom2Mar2023_ReturnsRegistrations()
    {
        // Arrange
        var date = new DateTime(2023, 03, 02);

        // Act
        var result = await _httpClient.GetAsync($"api/registrations/{date:yyyy-MM-dd}");

        // Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        var registrations = await result.Content.ReadFromJsonAsync<List<Registration>>();

        registrations?.Count.ShouldNotBe(0);
    }
}
