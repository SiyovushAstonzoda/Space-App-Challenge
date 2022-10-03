using Domain.Wrapper;
using Domain.Dtos;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IChallengeService
{
    Task<Response<List<GetChallengeDto>>> GetChallenges();
    Task<Response<AddChallengeDto>> AddChallenge(AddChallengeDto challenge);
    Task<Response<AddChallengeDto>> UpdateChallenge(AddChallengeDto challenge);
    Task<Response<string>> DeleteChallenge(int Id);
}
