using Testcontainers.MsSql;

namespace DotnetFriday.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class MsSqlTestContainerFixture : IAsyncLifetime 
{
	public MsSqlContainer Container { get; }

	public MsSqlTestContainerFixture()
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

	public MsSqlTestContainerFixture WithCompanies()
	{
		return this;
	}
}