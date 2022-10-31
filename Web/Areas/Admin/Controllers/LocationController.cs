using Domain.Dtos;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
namespace Web.Areas.Admin.Controllers;
[Area("Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locService)
        {
            _locationService = locService;
        }

        //Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var getLocations = await _locationService.GetLocations();
            return View(getLocations);
        }

        //Add
        [HttpGet]
        public IActionResult Add()
        {
            var emptyLocationDto = new AddLocationDto();
            return View(emptyLocationDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLocationDto addLocationDto)
        {
            if (ModelState.IsValid == false)
            {
                return View(addLocationDto);
            }
            await _locationService.AddLocation(addLocationDto);
            return RedirectToAction("Index");
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var getLocationDto = await _locationService.GetLocationById(id);
            return View(getLocationDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddLocationDto updateLocationDto)
        {
            if (ModelState.IsValid == false)
            {
                return View(updateLocationDto);
            }
            await _locationService.UpdateLocation(updateLocationDto);
            return RedirectToAction("Index");
        }

        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _locationService.DeleteLocation(id);
            return RedirectToAction("Index");
        }
    }
