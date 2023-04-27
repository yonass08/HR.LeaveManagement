using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence;

public class LeaveManagementDbContext: AuditableDbContext
{
    public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
    }

    public DbSet<LeaveType> LeaveTypes {get; set;}

    public DbSet<LeaveRequest> LeaveRequests {get; set;}

    public DbSet<LeaveAllocation> LeaveAllocations {get; set;}
    
}
