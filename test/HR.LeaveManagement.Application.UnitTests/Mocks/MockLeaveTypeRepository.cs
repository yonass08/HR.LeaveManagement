using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> CreateLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>(){
            new LeaveType()
            {
                Id = 1,
                Name = "Sick",
                DefaultDays = 12
            },
        
            new LeaveType()
            {
                Id = 2,
                Name = "Vacation",
                DefaultDays = 10
            },
            new LeaveType()
            {
                Id = 3,
                Name = "Holiday",
                DefaultDays = 3
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();
        mockRepo.Setup(r => r.GetAll()).ReturnsAsync(leaveTypes);
        mockRepo.Setup(r => r.Add(It.IsAny<LeaveType>()))
            .ReturnsAsync((LeaveType leaveType) => 
            {
                leaveTypes.Add(leaveType);
                return leaveType;
            });
        
        return mockRepo;

    }
}
