using Domain.Entities;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Services;

public class ChallengeService : IChallengeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ChallengeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<AddChallengeDto>> AddChallenge(AddChallengeDto challenge)
    {
        try
        {
            Challenge mapped = _mapper.Map<Challenge>(challenge);
            await _context.Challenges.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddChallengeDto>(_mapper.Map<AddChallengeDto>(mapped));
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

    // public async Task<Response<List<GetChallengeDto>>> GetChallenges()
    // {
    //     var challenges = await _context.Challenges.Select
    //     (l=> new GetChallengeDto()
    //     {
    //         Description = l.Description,
    //         Id = l.Id,
    //         Name = l.Name
    //     }
    //     ).ToListAsync();
    //     return new Response<List<GetChallengeDto>>(challenges);
    // }

    public async Task<Response<List<GetChallengeDto>>> GetChallenges()
    {
        var challenge = _mapper.Map<List<GetChallengeDto>>(await _context.Challenges.ToListAsync());
        return new Response<List<GetChallengeDto>>(challenge);
    }

     public async Task<Response<GetChallengeDto>> GetChallengeById(int id)
    {
        var result = _mapper.Map<GetChallengeDto>(await _context.Challenges.FindAsync(id));
        return new Response<GetChallengeDto>(result);
    }

    public async Task<Response<AddChallengeDto>> UpdateChallenge(AddChallengeDto challenge)
    {
        try
        {
            Challenge? mapped = _mapper.Map<Challenge>(challenge);
            _context.Challenges.Attach(mapped);
            _context.Entry(mapped).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new Response<AddChallengeDto>(_mapper.Map<AddChallengeDto>(mapped));
        }
        catch (Exception ex)
        {
            return new Response<AddChallengeDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
