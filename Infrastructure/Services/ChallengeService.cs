using Domain.Entities;
using Domain.Dtos;
using Domain.Wrapper;
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

    public async Task<Response<AddChallengeDto>> AddChallenge(AddChallengeDto model)
    {
        try
        {
            var challenge = new Challenge()
            {
                Description = model.Description,
                Name = model.Name
            };
            await _context.Challenges.AddAsync(challenge);
            await _context.SaveChangesAsync();
            model.Id = challenge.Id;
            return new Response<AddChallengeDto>(model);
        }
        catch (Exception ex)
        {
            return new Response<AddChallengeDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteChallenge(int Id)
    {
         try
        {
         var record = await _context.Challenges.FindAsync(Id);

         if(record == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "Not found");

         _context.Challenges.Remove(record);
         await _context.SaveChangesAsync();
         return new Response<string>("success");
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<List<GetChallengeDto>>> GetChallenges()
    {
        var challenges = await _context.Challenges.Select
        (l=> new GetChallengeDto()
        {
            Description = l.Description,
            Id = l.Id,
            Name = l.Name
        }
        ).ToListAsync();
        return new Response<List<GetChallengeDto>>(challenges);
    }

    public async Task<Response<AddChallengeDto>> UpdateChallenge(AddChallengeDto challenge)
    {
        try
        {
            var record = await _context.Challenges.FindAsync(challenge.Id);
             //if not found it will return null
            if(record == null) return new Response<AddChallengeDto>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.Name = challenge.Name;
            record.Name = challenge.Description;
            await _context.SaveChangesAsync();

            return new Response<AddChallengeDto>(challenge);
        }
        catch (Exception ex)
        {
            return new Response<AddChallengeDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
