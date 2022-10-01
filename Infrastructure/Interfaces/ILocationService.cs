using Domain.Wrapper;
using Domain.Entities;
namespace Infrastructure.Interfaces;

public interface ILocationService
{
    Task<Response<List<Location>>> GetLocations();
    Task<Response<Location>> AddLocation(Location location);
    Task<Response<Location>> UpdateLocation(Location location);
    Task<Response<string>> DeleteLocation(int Id);
}
