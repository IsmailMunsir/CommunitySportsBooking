using System.Security.Claims;
using CommunitySportsBooking.Models;
using CommunitySportsBooking.Models.ViewModels;
using CommunitySportsBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunitySportsBooking.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IFacilityService _facilityService;

        public ReviewController(IReviewService reviewService, IFacilityService facilityService)
        {
            _reviewService = reviewService;
            _facilityService = facilityService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllAsync();
            return View(reviews);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create(int facilityId, int? bookingId)
        {
            var facility = await _facilityService.GetByIdAsync(facilityId);
            if (facility == null) return NotFound();

            var model = new CreateReviewViewModel
            {
                FacilityId = facility.Id,
                FacilityName = facility.Name,
                BookingId = bookingId
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var review = new Review
            {
                FacilityId = model.FacilityId,
                MemberId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                BookingId = model.BookingId,
                Rating = model.Rating,
                Comment = model.Comment
            };

            await _reviewService.CreateAsync(review);

            TempData["SuccessMessage"] = "Thank you! Your review has been submitted.";
            return RedirectToAction(nameof(Index));
        }
    }
}