using Domain.Wrapper;
using Domain.Entities;
using Domain.Dtos;
namespace Infrastructure.Interfaces;

public interface ILocationService
{
    Task<Response<List<GetLocationDto>>> GetLocations();
    Task<Response<AddLocationDto>> AddLocation(AddLocationDto location);
    Task<Response<AddLocationDto>> UpdateLocation(AddLocationDto location);
    Task<Response<string>> DeleteLocation(int Id);
}
