using Domain.Wrapper;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IGroupService
{
    Task<Response<List<Group>>> GetGroups();
    Task<Response<Group>> AddGroup(Group group);
    Task<Response<Group>> UpdateGroup(Group group);
    Task<Response<string>> DeleteGroup(int Id);
}
