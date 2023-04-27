using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands;

public class CreateLeaveTypeCommandHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    private readonly CreateLeaveTypeCommandHandler _handler;

    public CreateLeaveTypeCommandHandlerTest()
    {
        _mockRepo = MockLeaveTypeRepository.CreateLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

    }

    [Fact]
    public async Task CreateValidLeaveTypeCommandTest()
    {
        var leaveTypeDto  = new CreateLeaveTypeDto(){Name = "Sick", DefaultDays = 10};
        var command = new CreateLeaveTypeCommand(){createLeaveTypeDto = leaveTypeDto};
        var result = await _handler.Handle(command, CancellationToken.None);

        result.ShouldBeOfType<int>();
        var leaveTypes = await _mockRepo.Object.GetAll();

        leaveTypes.Count.ShouldBe(4);
        
    }

    [Fact]
    public async Task CreateInvalidLeaveTypeCommandTest()
    {
        var leaveTypeDto  = new CreateLeaveTypeDto(){Name = "Sick", DefaultDays = -1};
        var command = new CreateLeaveTypeCommand(){createLeaveTypeDto = leaveTypeDto};

        ValidationException ex = await Should.ThrowAsync<ValidationException>
        (
            async () => 
            {
                await _handler.Handle(command, CancellationToken.None);
            }
        );

        var leaveTypes = await _mockRepo.Object.GetAll();
        leaveTypes.Count.ShouldBe(3);
        
    }
}
