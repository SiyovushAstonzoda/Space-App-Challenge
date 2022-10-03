using Domain.Entities;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class LocationService : ILocationService
{
    private readonly DataContext _context;
    public LocationService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<AddLocationDto>> AddLocation(AddLocationDto model)
    {
        try
        {
            var location = new Location()
            {
                Description = model.Description,
                Name = model.Name
            };
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            model.Id = location.Id;
            return new Response<AddLocationDto>(model);
        }
        catch (Exception ex)
        {
            return new Response<AddLocationDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<string>> DeleteLocation(int Id)
    {
        try
        {
         var record = await _context.Locations.FindAsync(Id);

         if(record == null) return new Response<string>(System.Net.HttpStatusCode.NotFound, "Not found");

         _context.Locations.Remove(record);
         await _context.SaveChangesAsync();
         return new Response<string>("success");
        }
        catch (Exception ex)
        {
            return new Response<string>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<List<GetLocationDto>>> GetLocations()
    {
        var locations = await _context.Locations.Select
        (
            l=> new GetLocationDto()
        {
            Name = l.Name,
            Id = l.Id,
            Description = l.Description
        }
        ).ToListAsync();
        return new Response<List<GetLocationDto>>(locations);
    }

    public async Task<Response<AddLocationDto>> UpdateLocation(AddLocationDto location)
    {
        try
        {
            var record = await _context.Locations.FindAsync(location.Id);
             //if not found it will return null
            if(record == null) return new Response<AddLocationDto>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.Name = location.Name;
            record.Description = location.Description;
            await _context.SaveChangesAsync();

            return new Response<AddLocationDto>(location);
        }
        catch (Exception ex)
        {
            return new Response<AddLocationDto>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
