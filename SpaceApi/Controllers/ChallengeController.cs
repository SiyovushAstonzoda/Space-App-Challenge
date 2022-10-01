using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace SpaceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChallengeController : ControllerBase
{
    private readonly IChallengeService _challengeService;
    public ChallengeController(IChallengeService challengeService)
    {
        _challengeService = challengeService;
    }

    [HttpGet]
    public async Task<Response<List<Challenge>>> GetChallenges()
    {
        return await _challengeService.GetChallenges();
    }

    [HttpPost]
    public async Task<Response<Challenge>> AddChallenge(Challenge challenge)
    {
        return await _challengeService.AddChallenge(challenge);
    }

    [HttpPut]
    public async Task<Response<Challenge>> UpdateChallenge(Challenge challenge)
    {
        return await _challengeService.UpdateChallenge(challenge);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteChallenge(int Id)
    {
        return await _challengeService.DeleteChallenge(Id);
    }
}
