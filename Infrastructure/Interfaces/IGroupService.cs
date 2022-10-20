using Domain.Wrapper;
using Domain.Entities;
using Domain.Dtos;
namespace Infrastructure.Interfaces;

public interface IGroupService
{
    Task<List<GetGroupDto>> GetGroups();
    Task<List<GetGroupDto>> GetGroupsWithParticipants();
    Task<AddGroupDto> GetGroupById(int id);
    Task<AddGroupDto> AddGroup(AddGroupDto groupDto);
    Task<AddGroupDto> UpdateGroup(AddGroupDto groupDto);
    Task<bool> DeleteGroup(int id);
}
