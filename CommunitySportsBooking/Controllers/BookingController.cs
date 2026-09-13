using System.Security.Claims;
using CommunitySportsBooking.Models;
using CommunitySportsBooking.Models.ViewModels;
using CommunitySportsBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunitySportsBooking.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IFacilityService _facilityService;

        public BookingController(IBookingService bookingService, IFacilityService facilityService)
        {
            _bookingService = bookingService;
            _facilityService = facilityService;
        }

        private int CurrentMemberId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> Create(int facilityId)
        {
            var facility = await _facilityService.GetByIdAsync(facilityId);
            if (facility == null) return NotFound();

            var model = new CreateBookingViewModel
            {
                FacilityId = facility.Id,
                FacilityName = facility.Name,
                PricePerHour = facility.PricePerHour
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (string.Compare(model.StartTime, model.EndTime, StringComparison.Ordinal) >= 0)
            {
                ModelState.AddModelError(nameof(model.EndTime), "End time must be after start time.");
                return View(model);
            }

            var available = await _bookingService.IsSlotAvailableAsync(
                model.FacilityId, model.BookingDate, model.StartTime, model.EndTime);

            if (!available)
            {
                ModelState.AddModelError(string.Empty, "This time slot is already booked. Please choose another time.");
                return View(model);
            }

            var booking = new Booking
            {
                MemberId = CurrentMemberId,
                FacilityId = model.FacilityId,
                BookingDate = model.BookingDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Notes = model.Notes,
                Status = BookingStatus.Confirmed
            };

            await _bookingService.CreateAsync(booking);

            TempData["SuccessMessage"] = $"Booking confirmed for {model.FacilityName} on {model.BookingDate:dd MMM yyyy}.";
            return RedirectToAction(nameof(MyBookings));
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            var bookings = await _bookingService.GetByMemberAsync(CurrentMemberId);
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null || booking.MemberId != CurrentMemberId) return NotFound();

            await _bookingService.UpdateStatusAsync(id, BookingStatus.Cancelled);
            TempData["SuccessMessage"] = "Booking cancelled.";
            return RedirectToAction(nameof(MyBookings));
        }
    }
}