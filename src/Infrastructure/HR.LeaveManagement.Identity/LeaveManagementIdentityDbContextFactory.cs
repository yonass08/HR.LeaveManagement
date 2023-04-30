using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HR.LeaveManagement.Identity;

public class LeaveManagementDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementIdentityDbContext>
{
    public LeaveManagementIdentityDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "../../../API/HR.LeaveManagement.Api")
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<LeaveManagementIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("LeaveManagementIdentityConnectionString");

        builder.UseNpgsql(connectionString);

        return new LeaveManagementIdentityDbContext(builder.Options);
    }
}
