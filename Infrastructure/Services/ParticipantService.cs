using Domain.Entities;
using Domain.Dtos;
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

    public async Task<AddParticipantDto> AddParticipant(AddParticipantDto participantDto)
    {
        var participant = new Participant
            {
                FullName = participantDto.FullName,
                Email = participantDto.Email,
                Phone = participantDto.Phone,
                Password = participantDto.Password,
                CreatedAt = participantDto.CreatedAt,
                GroupId = participantDto.GroupId,
                LocationId = participantDto.LocationId
            };

            await _context.Participants.AddAsync(participant);
            await _context.SaveChangesAsync();

            participantDto.Id = participant.Id;


             var participantCreated = await GetParticipantById(participant.Id);
             return participantCreated;
    }

    public async Task<bool> DeleteParticipant(int id)
    {
        var participant = await _context.Participants.FirstOrDefaultAsync(e => e.Id == id);

        if (participant == null)
        {
            return false;
        }

        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<GetParticipantDto>> GetParticipants()
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
        return participants;
    }

    public async Task<AddParticipantDto> GetParticipantById(int id)
    {
        var participant = await _context.Participants
            .Select(pa => new AddParticipantDto()
            {
                Id = pa.Id,
                FullName = pa.FullName,
                Email = pa.Email,
                Phone = pa.Phone,
                Password = pa.Password,
                CreatedAt = pa.CreatedAt,
                GroupId = pa.GroupId,
                LocationId = pa.LocationId,
            })
            .FirstOrDefaultAsync(tu => tu.Id == id);

        return participant;
    }

    public async Task<AddParticipantDto> UpdateParticipant(AddParticipantDto participantDto)
    {

            var participant = await _context.Participants.FirstOrDefaultAsync(pa => pa.Id == participantDto.Id);

            if (participant == null)
            {
                return null;
            }


            participant.FullName = participantDto.FullName;
            participant.Email = participantDto.Email;
            participant.Phone = participantDto.Phone;
            participant.Password = participantDto.Password;
            participant.CreatedAt = participantDto.CreatedAt;
            participant.GroupId = participantDto.GroupId;
            participant.LocationId = participantDto.LocationId;

            await _context.SaveChangesAsync();

            var participantUpdated = await GetParticipantById(participant.Id);
            return participantUpdated;
    }
}
