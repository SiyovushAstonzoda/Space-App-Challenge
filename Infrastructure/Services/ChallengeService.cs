using Domain.Entities;
using Domain.Dtos;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ChallengeService : IChallengeService
{
    private readonly DataContext _context;
    public ChallengeService(DataContext context)
    {
        _context = context;
    }

    public async Task<AddChallengeDto> AddChallenge(AddChallengeDto challengeDto)
    {
        var challenge = new Challenge
        {
            Name = challengeDto.Name,
            Description = challengeDto.Description
        };

        await _context.Challenges.AddAsync(challenge);
        await _context.SaveChangesAsync();

        challengeDto.Id = challenge.Id;

        var challengeCreated = await GetChallengeById(challenge.Id);
        return challengeCreated;
    }

    public async Task<bool> DeleteChallenge(int id)
    {
        var challenge = await _context.Challenges.FirstOrDefaultAsync(e => e.Id == id);

        if (challenge == null)
        {
            return false;
        }

        _context.Challenges.Remove(challenge);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<GetChallengeDto>> GetChallenges()
    {
        var challenges = await _context.Challenges
            .Select(l=> new GetChallengeDto
        {
            Id = l.Id,
            Description = l.Description,
            Name = l.Name
        }
        ).ToListAsync();
        return challenges;
    }

    public async Task<AddChallengeDto> GetChallengeById(int id)
    {
        var challenge = await _context.Challenges
            .Select(e => new AddChallengeDto()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            })
            .FirstOrDefaultAsync(e => e.Id == id);

        return challenge;
    }

    public async Task<AddChallengeDto> UpdateChallenge(AddChallengeDto challengeDto)
    {
        var challenge = await _context.Challenges.FirstOrDefaultAsync(e => e.Id == challengeDto.Id);

        if (challenge == null)
        {
            return null;
        }

        challenge.Name = challengeDto.Name;
        challenge.Description = challengeDto.Description;

        await _context.SaveChangesAsync();

        var challengeUpdated = await GetChallengeById(challenge.Id);
        return challengeUpdated;
    }
}
