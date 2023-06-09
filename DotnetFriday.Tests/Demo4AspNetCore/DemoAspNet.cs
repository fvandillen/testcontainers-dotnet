using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetFriday.Tests.Demo4AspNetCore;

public class DemoAspNet : IAsyncLifetime, IClassFixture<MsSqlTestContainerFixture>
{
	private WebApplicationFactory<Program> _factory;
	private readonly MsSqlTestContainerFixture _fixture;

	public DemoAspNet(MsSqlTestContainerFixture fixture)
	{
		_fixture = fixture;
	}
	
	public async Task InitializeAsync()
	{
		await _fixture.Container.StartAsync();
		Environment.SetEnvironmentVariable("Database:ConnectionString", _fixture.Container.GetConnectionString());
		Environment.SetEnvironmentVariable("Database:GenerateTestData", "true");
		_factory = new WebApplicationFactory<Program>();
	}

	public async Task DisposeAsync()
	{
		await _fixture.Container.StopAsync();
	}

	[Fact]
	public async Task Test()
	{
		var client = _factory.CreateClient();
		
		var response = await client.GetAsync($"/event/list");
	}
}