using Domain.Dtos;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly IChallengeService _challengeService;

        public ChallengeController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        //Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var getChallenges = await _challengeService.GetChallenges();
            return View(getChallenges);
        }

        //Add
        [HttpGet]
        public IActionResult Add()
        {
            var emptyChallengeDto = new AddChallengeDto();
            return View(emptyChallengeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddChallengeDto addChallengeDto)
        {
            if (ModelState.IsValid == false)
            {
                return View(addChallengeDto);
            }
            await _challengeService.AddChallenge(addChallengeDto);
            return RedirectToAction("Index");
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var getChallengeDto = await _challengeService.GetChallengeById(id);
            return View(getChallengeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddChallengeDto updateChallengeDto)
        {
            if (ModelState.IsValid == false)
            {
                return View(updateChallengeDto);
            }
            await _challengeService.UpdateChallenge(updateChallengeDto);
            return RedirectToAction("Index");
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _challengeService.DeleteChallenge(id);
            return RedirectToAction("Index");
        }
    }
}
