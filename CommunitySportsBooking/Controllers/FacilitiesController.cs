using CommunitySportsBooking.Models.ViewModels;
using CommunitySportsBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunitySportsBooking.Controllers
{
    [Authorize]
    public class FacilitiesController : Controller
    {
        private readonly IFacilityService _facilityService;
        private readonly IReviewService _reviewService;

        public FacilitiesController(IFacilityService facilityService, IReviewService reviewService)
        {
            _facilityService = facilityService;
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? facilityType, string? location, DateTime? date, string? time)
        {
            var model = new FacilitySearchViewModel
            {
                FacilityType = facilityType,
                Location = location,
                Date = date,
                Time = time,
                Results = await _facilityService.SearchAsync(facilityType, location)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var facility = await _facilityService.GetByIdAsync(id);
            if (facility == null) return NotFound();

            ViewBag.Reviews = await _reviewService.GetByFacilityAsync(id);
            ViewBag.AverageRating = await _reviewService.GetAverageRatingAsync(id);

            return View(facility);
        }
    }
}