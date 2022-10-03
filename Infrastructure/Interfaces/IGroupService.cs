using Domain.Wrapper;
using Domain.Entities;
using Domain.Dtos;
namespace Infrastructure.Interfaces;

public interface IGroupService
{
    Task<Response<List<GetGroupDto>>> GetGroups();
    Task<Response<AddGroupDto>> AddGroup(AddGroupDto group);
    Task<Response<AddGroupDto>> UpdateGroup(AddGroupDto group);
    Task<Response<string>> DeleteGroup(int Id);
}
