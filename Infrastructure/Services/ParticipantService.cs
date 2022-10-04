using Domain.Entities;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ParticipantService : IParticipantService
{
    private readonly DataContext _context;
    public ParticipantService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<AddParticipantDto>> AddParticipant(AddParticipantDto model)
    {
        try
        {
            var participant = new Participant()
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password,
                CreatedAt = model.CreatedAt,
                GroupId = model.GroupId,
                LocationId = model.LocationId
            };
            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();
            model.Id = participant.Id;
            return new Response<AddParticipantDto>(model);
        }
        catch (Exception ex)
        {
            return new Response<AddParticipantDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
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

    public async Task<Response<List<GetParticipantDto>>> GetParticipants()
    {
         var participants = await (from pa in _context.Participants
                           join gr in _context.Groups
                           on pa.GroupId equals gr.Id
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
                                GroupName = gr.GroupNick,
                                LocationName = lo.Name
                           }).ToListAsync();
        return new Response<List<GetParticipantDto>>(participants);
    }

    public async Task<Response<AddParticipantDto>> UpdateParticipant(AddParticipantDto participant)
    {
        try
        {
            var record = await _context.Participants.FindAsync(participant.Id);
             //if not found it will return null
            if(record == null) return new Response<AddParticipantDto>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.FullName = participant.FullName;
            record.Email = participant.Email;
            record.Phone = participant.Phone;
            record.Password = participant.Password;
            record.CreatedAt = participant.CreatedAt;
            record.GroupId = participant.GroupId;
            record.LocationId = participant.LocationId;
            await _context.SaveChangesAsync();

            return new Response<AddParticipantDto>(participant);
        }
        catch (Exception ex)
        {
            return new Response<AddParticipantDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
