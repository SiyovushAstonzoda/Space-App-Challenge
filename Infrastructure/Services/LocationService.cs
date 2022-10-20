using Domain.Entities;
using Domain.Dtos;
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

    public async Task<AddLocationDto> AddLocation(AddLocationDto locationDto)
    {
        var location = new Location
            {
                Name = locationDto.Name,
                Description = locationDto.Description
            };
        
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            locationDto.Id = location.Id;

        var challengeCreated = await GetLocationById(location.Id);
        return challengeCreated;
    }

    public async Task<bool> DeleteLocation(int id)
    {
        var location = await _context.Locations.FirstOrDefaultAsync(e => e.Id == id);

        if (location == null)
        {
            return false;
        }

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<GetLocationDto>> GetLocations()
    {
        var locations = await _context.Locations
            .Select
        (
            l=> new GetLocationDto()
        {
            Id = l.Id,
            Name = l.Name,
            Description = l.Description
        }
        ).ToListAsync();
        return locations;
    }

    public async Task<AddLocationDto> GetLocationById(int id)
    {
        var location = await _context.Locations
            .Select(lo => new AddLocationDto()
            {
                Id = lo.Id,
                Name = lo.Name,
                Description = lo.Description
            })
            .FirstOrDefaultAsync(tu => tu.Id == id);

        return location;
    }

    public async Task<AddLocationDto> UpdateLocation(AddLocationDto locationDto)
    {
        var location = await _context.Locations.FirstOrDefaultAsync(e => e.Id == locationDto.Id);

        if (location == null)
        {
            return null;
        }

        location.Name = locationDto.Name;
        location.Description = locationDto.Description;

        await _context.SaveChangesAsync();

        var locationUpdated = await GetLocationById(location.Id);
        return locationUpdated;
    }
}
