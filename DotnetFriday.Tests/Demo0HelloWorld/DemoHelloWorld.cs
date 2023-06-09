using System.Diagnostics;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace DotnetFriday.Tests.Demo0HelloWorld;

public class DemoHelloWorld : IAsyncLifetime
{
	private IContainer _container;
	
	public DemoHelloWorld()
	{
		_container = new ContainerBuilder()
			.WithImage("testcontainers/helloworld:1.1.0")
			.WithPortBinding(8080, true)
			.Build();
	}
	public async Task InitializeAsync()
	{
		await _container.StartAsync();
	}

	public async Task DisposeAsync()
	{
		await _container.StopAsync();
	}

	[Fact]
	public async Task SendRequest()
	{
		var httpClient = new HttpClient();
		var requestUri = new UriBuilder(Uri.UriSchemeHttp, _container.Hostname, _container.GetMappedPublicPort(8080), "uuid").Uri;
		var guid = await httpClient
			.GetStringAsync(requestUri)
			.ConfigureAwait(false);
		
		Assert.True(Guid.TryParse(guid, out _));
	}
}