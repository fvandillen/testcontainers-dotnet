using DotNet.Testcontainers.Containers;
using Microsoft.Data.SqlClient;

namespace FourDotnet.IntegrationTesting.Tests;

public sealed class BasicTest : IAsyncLifetime
{
    private readonly MsSqlTestcontainer _container;

    public BasicTest()
    {
        _container = TestUtils.BuildContainer();
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public async Task DisposeAsync() => await _container.DisposeAsync();
    
    [Fact]
    public void SimpleSelect()
    {
        using (var con = TestUtils.GetSqlConnection(_container))
        {
            con.Open();

            using (var command = new SqlCommand("SELECT 1", con))
            {
                var reader = command.ExecuteReader();
                reader.Read();
                var numberOne = reader.GetInt32(0);
                
                Assert.Equal(1, numberOne);
            }
        }
    }
}