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
// public class ParticipantController : ControllerBase
// {
//     private readonly IParticipantService _participantService;
//     public ParticipantController(IParticipantService participantService)
//     {
//         _participantService = participantService;
//     }

//      [HttpGet]
//     public async Task<Response<List<GetParticipantDto>>> GetParticipants()
//     {
//         return await _participantService.GetParticipants();
//     }

//     [HttpPost]
//     public async Task<Response<AddParticipantDto>> AddParticipant(AddParticipantDto participant)
//     {
//         return await _participantService.AddParticipant(participant);
//     }

//     [HttpPut]
//     public async Task<Response<AddParticipantDto>> UpdateParticipant(AddParticipantDto participant)
//     {
//         return await _participantService.UpdateParticipant(participant);
//     }

//     [HttpDelete]
//     public async Task<Response<string>> DeleteParticipant(int Id)
//     {
//         return await _participantService.DeleteParticipant(Id);
//     }
// }
