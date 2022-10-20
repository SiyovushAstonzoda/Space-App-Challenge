using Domain.Entities;
using Domain.Dtos;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GroupService : IGroupService
{
    private readonly DataContext _context;
    public GroupService(DataContext context)
    {
        _context = context;
    }

    public async Task<AddGroupDto> AddGroup(AddGroupDto groupDto)
    {
        var group = new Group
        {
            GroupNick = groupDto.GroupNick,
            NeededMember = groupDto.NeededMember,
            TeamSlogan = groupDto.TeamSlogan,
            CreatedAt = groupDto.CreatedAt,
            ChallengeId = groupDto.ChallengeId,
        };

        await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync();

        groupDto.Id = group.Id;

        var challengeCreated = await GetGroupById(group.Id);
        return challengeCreated;
    }

    public async Task<bool> DeleteGroup(int id)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(e => e.Id == id);

        if (group == null)
        {
            return false;
        }

        _context.Groups.Remove(group);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<GetGroupDto>> GetGroups()
    {
       var groups = await (from gr in _context.Groups
                           join ch in _context.Challenges
                           on gr.ChallengeId equals ch.Id
                           select new GetGroupDto
                           {
                                ChallengeId = ch.Id,
                                ChallengeName = ch.Name,
                                GroupNick = gr.GroupNick,
                                NeededMember = gr.NeededMember,
                                TeamSlogan = gr.TeamSlogan,
                                Id = gr.Id
                           }).ToListAsync();
        return groups;
    }

    public async Task<AddGroupDto> GetGroupById(int id)
    {
        var group = await _context.Groups
            .Select(gr => new AddGroupDto()
            {
                Id = gr.Id,
                GroupNick = gr.GroupNick,
                NeededMember = gr.NeededMember,
                TeamSlogan = gr.TeamSlogan,
                CreatedAt = gr.CreatedAt,
                ChallengeId = gr.ChallengeId
            })
            .FirstOrDefaultAsync(tu => tu.Id == id);

        return group;
    }

    public async Task<List<GetGroupDto>> GetGroupsWithParticipants()
    {
       var groups = await
       (
            from gr in _context.Groups
            join ch in _context.Challenges
            on gr.ChallengeId equals ch.Id
            select new GetGroupDto()
            {
                ChallengeId = ch.Id,
                ChallengeName = ch.Name,
                GroupNick = gr.GroupNick,
                NeededMember = gr.NeededMember,
                TeamSlogan = gr.TeamSlogan,
                Id = gr.Id,
                Participants = 
                (
                    from pa in _context.Participants
                    where gr.Id == pa.GroupId
                    join lo in _context.Locations
                    on pa.LocationId equals lo.Id
                    select new GetParticipantDto
                    {
                        Id = pa.Id,
                        FullName = pa.FullName,
                        Email = pa.Email,
                        Phone = pa.Phone,
                        Password = pa.Password,
                        CreatedAt = pa.CreatedAt,
                        GroupId = gr.Id,
                        GroupName = gr.GroupNick,
                        LocationId = lo.Id,
                        LocationName = lo.Name
                    }
                ).ToList(),
            }
            ).ToListAsync();
        return groups;
    }

    public async Task<AddGroupDto> UpdateGroup(AddGroupDto groupDto)
    {
        var group = await _context.Groups.FirstOrDefaultAsync(e => e.Id == groupDto.Id);

        if (group == null)
        {
            return null;
        }

        group.GroupNick = groupDto.GroupNick;
        group.NeededMember = groupDto.NeededMember;
        group.TeamSlogan = groupDto.TeamSlogan;
        group.CreatedAt = groupDto.CreatedAt;
        group.ChallengeId = groupDto.ChallengeId;

        await _context.SaveChangesAsync();

        var groupUpdated = await GetGroupById(group.Id);
        return groupUpdated;
    }
}
