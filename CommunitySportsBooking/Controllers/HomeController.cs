using System.Diagnostics;
using CommunitySportsBooking.Models;
using CommunitySportsBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommunitySportsBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFacilityService _facilityService;
        private readonly IReviewService _reviewService;

        public HomeController(IFacilityService facilityService, IReviewService reviewService)
        {
            _facilityService = facilityService;
            _reviewService = reviewService;
        }

        // GET: / — entry point for all users (members + guests)
        public async Task<IActionResult> Index()
        {
            var facilities = await _facilityService.GetAllAsync();
            ViewBag.FeaturedFacilities = facilities.Take(3).ToList();

            var reviews = await _reviewService.GetAllAsync();
            ViewBag.RecentReviews = reviews.Take(3).ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}