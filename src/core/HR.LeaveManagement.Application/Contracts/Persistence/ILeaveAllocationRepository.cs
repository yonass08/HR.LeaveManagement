using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveAllocationRepository: IGenericRepository<LeaveAllocation>
{
    public Task<LeaveAllocation> GetLeaveAllocationWithDetails(int Id);

    public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();

}
