using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
namespace Infrastructure.Services;

public class ChallengeService : IChallengeService
{
    private readonly DataContext _context;
    public ChallengeService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<Challenge>> AddChallenge(Challenge challenge)
    {
        try
        {
            await _context.Challenges.AddAsync(challenge);
            await _context.SaveChangesAsync();
            return new Response<Challenge>(challenge);
        }
        catch (Exception ex)
        {
            return new Response<Challenge>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
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

    public async Task<Response<List<Challenge>>> GetChallenges()
    {
        //pod voprosom
        var result = _context.Challenges.ToList();
        return new Response<List<Challenge>>(result.ToList());
    }

    public async Task<Response<Challenge>> UpdateChallenge(Challenge challenge)
    {
        try
        {
            var record = await _context.Challenges.FindAsync(challenge.Id);
             //if not found it will return null
            if(record == null) return new Response<Challenge>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.Name = challenge.Name;
            record.Name = challenge.Description;
            await _context.SaveChangesAsync();

            return new Response<Challenge>(record);
        }
        catch (Exception ex)
        {
            return new Response<Challenge>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
