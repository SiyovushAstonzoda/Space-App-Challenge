using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
namespace Infrastructure.Services;

public class GroupService : IGroupService
{
    private readonly DataContext _context;
    public GroupService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<Group>> AddGroup(Group group)
    {
        try
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return new Response<Group>(group);
        }
        catch (Exception ex)
        {
            return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
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

    public async Task<Response<List<Group>>> GetGroups()
    {
       //pod voprosom
        var result =  _context.Groups.ToList();
        return new Response<List<Group>>(result.ToList());
    }

    public async Task<Response<Group>> UpdateGroup(Group group)
    {
        try
        {
            var record = await _context.Groups.FindAsync(group.Id);
             //if not found it will return null
            if(record == null) return new Response<Group>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.GroupNick = group.GroupNick;
            record.NeededMember = group.NeededMember;
            record.TeamSlogan = group.TeamSlogan;
            record.CreatedAt = group.CreatedAt;
            record.ChallengeId = group.ChallengeId;
            await _context.SaveChangesAsync();

            return new Response<Group>(record);
        }
        catch (Exception ex)
        {
            return new Response<Group>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
