using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository: IGenericRepository<LeaveRequest>
{
    public Task<LeaveRequest> GetLeaveRequestWithDetails(int Id);

    public Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();

    public Task ChangeApprovalStatus(int Id, bool? ApprovalStatus);

}
