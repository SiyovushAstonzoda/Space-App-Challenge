using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
namespace Infrastructure.Services;

public class ParticipantService : IParticipantService
{
    private readonly DataContext _context;
    public ParticipantService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<Participant>> AddParticipant(Participant participant)
    {
        try
        {
            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();
            return new Response<Participant>(participant);
        }
        catch (Exception ex)
        {
            return new Response<Participant>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteParticipant(int Id)
    {
         try
        {
         var record = await _context.Participants.FindAsync(Id);

         if(record == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "Not found");

         _context.Participants.Remove(record);
         await _context.SaveChangesAsync();
         return new Response<string>("success");
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<List<Participant>>> GetParticipants()
    {
        //pod voprosom
        var result =  _context.Participants.ToList();
        return new Response<List<Participant>>(result.ToList());
    }

    public async Task<Response<Participant>> UpdateParticipant(Participant participant)
    {
        try
        {
            var record = await _context.Participants.FindAsync(participant.Id);
             //if not found it will return null
            if(record == null) return new Response<Participant>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.FullName = participant.FullName;
            record.Email = participant.Email;
            record.Phone = participant.Phone;
            record.Password = participant.Password;
            record.CreatedAt = participant.CreatedAt;
            record.GroupId = participant.GroupId;
            record.LocationId = participant.LocationId;
            await _context.SaveChangesAsync();

            return new Response<Participant>(record);
        }
        catch (Exception ex)
        {
            return new Response<Participant>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
