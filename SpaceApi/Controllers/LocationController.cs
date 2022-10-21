// using Domain.Entities;
// using Domain.Dtos;
// using Domain.Wrapper;
// using Infrastructure.Context;
// using Infrastructure.Interfaces;
// using Infrastructure.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace SpaceApi.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// public class LocationController : ControllerBase
// {
//     private readonly ILocationService _locationService;
//     public LocationController(ILocationService locationService)
//     {
//         _locationService = locationService;
//     }

//     [HttpGet]
//     public async Task<Response<List<GetLocationDto>>> GetLocations()
//     {
//         return await _locationService.GetLocations();
//     }

//     [HttpPost]
//     public async Task<Response<AddLocationDto>> AddLocation(AddLocationDto location)
//     {
//         return await _locationService.AddLocation(location);
//     }

//     [HttpPut]
//     public async Task<Response<AddLocationDto>> UpdateLocation(AddLocationDto location)
//     {
//         return await _locationService.UpdateLocation(location);
//     }

//     [HttpDelete]
//     public async Task<Response<string>> DeleteLocation(int Id)
//     {
//         return await _locationService.DeleteLocation(Id);
//     }
// }
