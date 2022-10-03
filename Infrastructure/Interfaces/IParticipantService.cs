using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IParticipantService
{
    Task<Response<List<GetParticipantDto>>> GetParticipants();
    Task<Response<AddParticipantDto>> AddParticipant(AddParticipantDto participant);
    Task<Response<AddParticipantDto>> UpdateParticipant(AddParticipantDto participant);
    Task<Response<string>> DeleteParticipant(int Id);
}
