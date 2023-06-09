using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace DotnetFriday.Tests.Demo1Simple;

public class DemoSimple : IAsyncLifetime
{
	public MsSqlContainer Container { get; }
	
	public DemoSimple()
	{
		Container = new MsSqlBuilder()
			.WithPassword(Guid.NewGuid().ToString("D"))
			.Build();
	}

	public async Task InitializeAsync()
	{
		await Container.StartAsync();
	}

	public async Task DisposeAsync()
	{
		await Container.StopAsync();
	}

	[Fact]
	public async Task GetNumber()
	{
		var connectionString = Container.GetConnectionString();
		var connection = new SqlConnection(connectionString);
		await connection.OpenAsync();
		
		var command = new SqlCommand("SELECT 2", connection);
		var res = await command.ExecuteScalarAsync();
		Assert.Equal(2, res);
	}
	
	[Fact]
	public async Task AddNumbers()
	{
		var connectionString = Container.GetConnectionString();
		var connection = new SqlConnection(connectionString);
		await connection.OpenAsync();
		
		var command = new SqlCommand("SELECT 2+2", connection);
		var res = await command.ExecuteScalarAsync();
		Assert.Equal(4, res);
	}
}