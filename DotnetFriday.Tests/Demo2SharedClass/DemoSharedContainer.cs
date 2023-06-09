using Microsoft.Data.SqlClient;
using Xunit.Abstractions;

namespace DotnetFriday.Tests.Demo2SharedClass;

public class DemoSharedContainer : IClassFixture<MsSqlTestContainerFixture>
{
	private readonly MsSqlTestContainerFixture _fixture;
	private readonly ITestOutputHelper _output;
	
	public DemoSharedContainer(MsSqlTestContainerFixture fixture, ITestOutputHelper output)
	{
		_fixture = fixture;
		_output = output;
	}

	[Fact]
	private async Task ConnectAndSelect()
	{
		var connectionString = _fixture.Container.GetConnectionString();
		var connection = new SqlConnection(connectionString);
		await connection.OpenAsync();
		
		var command = new SqlCommand("SELECT 9000", connection);
		var result = await command.ExecuteScalarAsync();
		Assert.Equal(9000, result);
	}
	
	[Fact]
	private void BarTest()
	{
		_output.WriteLine(_fixture.Container.GetConnectionString());
	}
}