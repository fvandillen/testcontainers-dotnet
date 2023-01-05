using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FourDotnet.IntegrationTesting.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FourDotnet.IntegrationTesting.Tests;

public static class TestUtils
{
    public static MsSqlTestcontainer BuildContainer()
    { var container = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithEnvironment("ACCEPT_EULA", "Y")
            //.WithNetwork(network)
            .WithDatabase(new MsSqlTestcontainerConfiguration()
            {
                Password = "UltraSecret92120!!"
            })
            .Build();

        return container;
    }
    
    public static ExampleContext GetDatabaseContext(MsSqlTestcontainer container)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExampleContext>();
        optionsBuilder.UseSqlServer(BuildInsecureConnectionString(container));
        return new ExampleContext(optionsBuilder.Options);
    }

    public static SqlConnection GetSqlConnection(MsSqlTestcontainer container)
    {
        return new SqlConnection(BuildInsecureConnectionString(container));
    }
    
    private static string BuildInsecureConnectionString(MsSqlTestcontainer container)
    {
        return $"{container.ConnectionString};TrustServerCertificate=yes;";
    }
}