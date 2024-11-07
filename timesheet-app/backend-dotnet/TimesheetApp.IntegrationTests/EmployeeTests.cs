using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using TimesheetApp.Domain.Models;

namespace TimesheetApp.IntegrationTests;

public class EmployeeTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public EmployeeTests(WebApplicationFactory<Program> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Get_EmplyeesInDatabase_ReturnsEmployees()
    {
        // Arrange

        // Act
        var result = await _httpClient.GetAsync("api/employees");

        // Assert
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        var employees = await result.Content.ReadFromJsonAsync<List<Employee>>();
        employees.ShouldNotBeNull();
    }
}
