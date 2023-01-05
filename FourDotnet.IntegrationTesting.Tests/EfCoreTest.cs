using DotNet.Testcontainers.Containers;
using FourDotnet.IntegrationTesting.Database;
using FourDotnet.IntegrationTesting.Services;
using Microsoft.EntityFrameworkCore;

namespace FourDotnet.IntegrationTesting.Tests;

public sealed class EfCoreTest : IAsyncLifetime
{
    /// <summary>
    /// Docker container with SQL server.
    /// </summary>
    private readonly MsSqlTestcontainer _container;
    
    public EfCoreTest()
    {
        _container = TestUtils.BuildContainer();
    }
    
    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        
        // Ensure DB is created.
        await using var db = TestUtils.GetDatabaseContext(_container);
        await db.Database.EnsureCreatedAsync();

        // Insert test data.
        var employee1 = new Employee { FirstName = "John", Lastname = "Doe", BankBalance = 1000, Salary = 4000};
        var employee2 = new Employee { FirstName = "Philip", Lastname = "Fast", BankBalance = 9000, Salary = 1000 };

        await db.AddRangeAsync(employee1, employee2);
        await db.SaveChangesAsync();
    }

    public async Task DisposeAsync() => await _container.DisposeAsync();

    [Fact]
    public async Task Database_ContainsTwoEmployees()
    {
        await using var db = TestUtils.GetDatabaseContext(_container);

        var count = await db.Employees.CountAsync();
        Assert.Equal(2, count);
    }
    
    [Fact]
    public async Task Database_SelectJohnDoe()
    {
        await using var db = TestUtils.GetDatabaseContext(_container);

        var johnDoe = await db.Employees.SingleAsync(x => x.FirstName == "John" && x.Lastname == "Doe");
        
        Assert.NotNull(johnDoe);
        Assert.Equal("John", johnDoe.FirstName);
        Assert.Equal("Doe", johnDoe.Lastname);
        Assert.Equal(1000, johnDoe.BankBalance);
        Assert.Equal(4000, johnDoe.Salary);
    }
    
    [Fact]
    public async Task Database_PayEmployees()
    {
        await using var db = TestUtils.GetDatabaseContext(_container);
        
        var service = new EmployeeService(db);
        await service.Pay();
        var employees = await service.List();

        var employee1 = employees.Single(x => x.FirstName == "John");
        var employee2 = employees.Single(x => x.FirstName == "Philip");
        
        Assert.Equal(5000, employee1.BankBalance);
        Assert.Equal(10000, employee2.BankBalance);
    }
}