using Domain.Wrapper;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IParticipantService
{
    Task<Response<List<Participant>>> GetParticipants();
    Task<Response<Participant>> AddParticipant(Participant participant);
    Task<Response<Participant>> UpdateParticipant(Participant participant);
    Task<Response<string>> DeleteParticipant(int Id);
}
