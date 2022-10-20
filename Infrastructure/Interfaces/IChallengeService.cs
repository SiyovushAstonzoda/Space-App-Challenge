using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IChallengeService
{
    Task<List<GetChallengeDto>> GetChallenges();
    Task<AddChallengeDto> GetChallengeById(int id);
    Task<AddChallengeDto> AddChallenge(AddChallengeDto challengeDto);
    Task<AddChallengeDto> UpdateChallenge(AddChallengeDto challengeDto);
    Task<bool> DeleteChallenge(int id);
}
