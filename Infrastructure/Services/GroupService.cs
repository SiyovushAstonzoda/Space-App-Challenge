using Domain.Entities;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using global::AutoMapper;

namespace Infrastructure.Services;

public class GroupService : IGroupService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public GroupService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<AddGroupDto>> AddGroup(AddGroupDto group)
    {
        try
        {
            Group? mapped = _mapper.Map<Group>(group);
            await _context.Groups.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddGroupDto>(_mapper.Map<AddGroupDto>(mapped));
        }
        catch (Exception ex)
        {
            return new Response<AddGroupDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteGroup(int Id)
    {
        try
        {
         var record = await _context.Groups.FindAsync(Id);

         if(record == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "Not found");

         _context.Groups.Remove(record);
         await _context.SaveChangesAsync();
         return new Response<string>("success");
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<List<GetGroupDto>>> GetGroups()
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
        return new Response<List<GetGroupDto>>(groups);
    }

    public async Task<Response<List<GetGroupDto>>> GetGroupsWithParticipants()
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
        return new Response<List<GetGroupDto>>(groups);
    }

    public async Task<Response<AddGroupDto>> UpdateGroup(AddGroupDto group)
    {
        try
        {
            Group? mapped = _mapper.Map<Group>(group);
            _context.Groups.Attach(mapped);
            _context.Entry(mapped).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new Response<AddGroupDto>(_mapper.Map<AddGroupDto>(mapped));
        }
        catch (Exception ex)
        {
            return new Response<AddGroupDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
