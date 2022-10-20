using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IParticipantService
{
    Task<List<GetParticipantDto>> GetParticipants();
    Task<AddParticipantDto> GetParticipantById(int id);
    Task<AddParticipantDto> AddParticipant(AddParticipantDto participantDto);
    Task<AddParticipantDto> UpdateParticipant(AddParticipantDto participantDto);
    Task<bool> DeleteParticipant(int id);
}
