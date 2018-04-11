using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Repository>
{
    Repository IDesignTimeDbContextFactory<Repository>.CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<Repository>();

        var connectionString = configuration.GetConnectionString("DbContext");

        builder.UseNpgsql(connectionString);

        return new Repository(builder.Options);
    }
}