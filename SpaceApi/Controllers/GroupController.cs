using Domain.Entities;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace SpaceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;
    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpGet("GetGroups")]
    public async Task<Response<List<GetGroupDto>>> GetGroups()
    {
        return await _groupService.GetGroups();
    }

    [HttpGet("GetGroupsWithParticipants")]
    public async Task<Response<List<GetGroupDto>>> GetGroupsWithParticipants()
    {
        return await _groupService.GetGroupsWithParticipants();
    }

    [HttpPost]
    public async Task<Response<AddGroupDto>> AddGroup(AddGroupDto group)
    {
        return await _groupService.AddGroup(group);
    }

    [HttpPut]
    public async Task<Response<AddGroupDto>> UpdateGroup(AddGroupDto group)
    {
        return await _groupService.UpdateGroup(group);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteGroup(int Id)
    {
        return await _groupService.DeleteGroup(Id);
    }
}
