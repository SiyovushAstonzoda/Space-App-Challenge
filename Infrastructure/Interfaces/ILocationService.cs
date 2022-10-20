using Domain.Wrapper;
using Domain.Entities;
using Domain.Dtos;
namespace Infrastructure.Interfaces;

public interface ILocationService
{
    Task<List<GetLocationDto>> GetLocations();
    Task<AddLocationDto> GetLocationById(int id);
    Task<AddLocationDto> AddLocation(AddLocationDto locationDto);
    Task<AddLocationDto> UpdateLocation(AddLocationDto locationDto);
    Task<bool> DeleteLocation(int id);
}
