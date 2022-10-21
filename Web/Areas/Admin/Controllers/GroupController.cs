using Domain.Dtos;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Areas.Admin.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IChallengeService _challengeService;

        public GroupController(IGroupService groupService, IChallengeService challengeService)
        {
            _groupService = groupService;
            _challengeService = challengeService;
        }

        //Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var getGroups = await _groupService.GetGroups();
            return View(getGroups);
        }

        //Add
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Challenges = await _challengeService.GetChallenges();
            var emptyGroupDto = new AddGroupDto();
            return View(emptyGroupDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGroupDto addGroupDto)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Challenges = _challengeService.GetChallenges();
                return View(addGroupDto);
            }
            await _groupService.AddGroup(addGroupDto);
            return RedirectToAction("Index");
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Challenges = _challengeService.GetChallenges();
            var getGroupDto = await _groupService.GetGroupById(id);
            return View(getGroupDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddGroupDto updateGroupDto)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Challenges = _challengeService.GetChallenges();
                return View(updateGroupDto);
            }
            await _groupService.UpdateGroup(updateGroupDto);
            return RedirectToAction("Index");
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.DeleteGroup(id);
            return RedirectToAction("Index");
        }
    }
}
