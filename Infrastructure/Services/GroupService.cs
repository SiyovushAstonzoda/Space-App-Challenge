using Domain.Entities;
using Domain.Dtos;
using Domain.Wrapper;
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

    public async Task<Response<AddGroupDto>> AddGroup(AddGroupDto model)
    {
        try
        {
            var group = new Group()
            {
                GroupNick = model.GroupNick,
                NeededMember = model.NeededMember,
                TeamSlogan = model.TeamSlogan,
                CreatedAt = model.CreatedAt,
                ChallengeId = model.ChallengeId
            };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return new Response<AddGroupDto>(model);
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

    public async Task<Response<AddGroupDto>> UpdateGroup(AddGroupDto group)
    {
        try
        {
            var record = await _context.Groups.FindAsync(group.Id);
             //if not found it will return null
            if(record == null) return new Response<AddGroupDto>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.GroupNick = group.GroupNick;
            record.NeededMember = group.NeededMember;
            record.TeamSlogan = group.TeamSlogan;
            record.CreatedAt = group.CreatedAt;
            record.ChallengeId = group.ChallengeId;
            await _context.SaveChangesAsync();

            return new Response<AddGroupDto>(group);
        }
        catch (Exception ex)
        {
            return new Response<AddGroupDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
