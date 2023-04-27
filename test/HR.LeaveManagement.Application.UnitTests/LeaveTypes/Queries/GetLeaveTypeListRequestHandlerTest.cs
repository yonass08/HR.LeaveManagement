using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;
using System;
using System.Collections.Generic;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Queries;

public class GetLeaveTypeListRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    public GetLeaveTypeListRequestHandlerTest()
    {
        _mockRepo = MockLeaveTypeRepository.CreateLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(c => 
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var Handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);

        var result = await Handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}
