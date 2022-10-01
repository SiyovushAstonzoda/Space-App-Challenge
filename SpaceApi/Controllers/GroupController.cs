using Domain.Entities;
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

    [HttpGet]
    public async Task<Response<List<Group>>> GetGroups()
    {
        return await _groupService.GetGroups();
    }

    [HttpPost]
    public async Task<Response<Group>> AddGroup(Group group)
    {
        return await _groupService.AddGroup(group);
    }

    [HttpPut]
    public async Task<Response<Group>> UpdateGroup(Group group)
    {
        return await _groupService.UpdateGroup(group);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteGroup(int Id)
    {
        return await _groupService.DeleteGroup(Id);
    }
}
