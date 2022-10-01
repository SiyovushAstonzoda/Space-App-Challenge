using Domain.Wrapper;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface IChallengeService
{
    Task<Response<List<Challenge>>> GetChallenges();
    Task<Response<Challenge>> AddChallenge(Challenge challenge);
    Task<Response<Challenge>> UpdateChallenge(Challenge challenge);
    Task<Response<string>> DeleteChallenge(int Id);
}
