using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
namespace Infrastructure.Services;

public class LocationService : ILocationService
{
    private readonly DataContext _context;
    public LocationService(DataContext context)
    {
        _context = context;
    }

    public async Task<Response<Location>> AddLocation(Location location)
    {
        try
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            return new Response<Location>(location);
        }
        catch (Exception ex)
        {
            return new Response<Location>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
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

    public async Task<Response<List<Location>>> GetLocations()
    {
        //pod voprosom
        var result =  _context.Locations.ToList();
        return new Response<List<Location>>(result.ToList());
    }

    public async Task<Response<Location>> UpdateLocation(Location location)
    {
        try
        {
            var record = await _context.Locations.FindAsync(location.Id);
             //if not found it will return null
            if(record == null) return new Response<Location>(System.Net.HttpStatusCode.NotFound, "No record found");

            //pod voprosom
            record.Name = location.Name;
            record.Description = location.Description;
            await _context.SaveChangesAsync();

            return new Response<Location>(record);
        }
        catch (Exception ex)
        {
            return new Response<Location>(System.Net.HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
