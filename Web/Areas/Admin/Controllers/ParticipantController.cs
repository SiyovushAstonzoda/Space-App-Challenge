using Domain.Dtos;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
namespace Web.Areas.Admin.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        //Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var getParticipants = await _participantService.GetParticipants();
            return View(getParticipants);
        }

        //Add
        [HttpGet]
        public IActionResult Add()
        {
            var emptyParticipantDto = new AddParticipantDto();
            return View(emptyParticipantDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddParticipantDto addParticipantDto)
        {
            if (ModelState.IsValid == false)
            {
                return View(addParticipantDto);
            }
            await _participantService.AddParticipant(addParticipantDto);
            return RedirectToAction("Index");
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var getParticipantDto = await _participantService.GetParticipantById(id);
            return View(getParticipantDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddParticipantDto updateParticipantDto)
        {
            if (ModelState.IsValid == false)
            {
                return View(updateParticipantDto);
            }
            await _participantService.UpdateParticipant(updateParticipantDto);
            return RedirectToAction("Index");
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _participantService.DeleteParticipant(id);
            return RedirectToAction("Index");
        }
    }
}
