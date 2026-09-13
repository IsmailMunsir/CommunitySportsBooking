using CommunitySportsBooking.Models;
using CommunitySportsBooking.Models.ViewModels;
using CommunitySportsBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunitySportsBooking.Controllers
{
    [AllowAnonymous]
    public class GuestController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IFacilityService _facilityService;
        private readonly IInquiryService _inquiryService;

        public GuestController(IMemberService memberService, IFacilityService facilityService, IInquiryService inquiryService)
        {
            _memberService = memberService;
            _facilityService = facilityService;
            _inquiryService = inquiryService;
        }

        [HttpGet]
        public IActionResult BecomeMember() => View(new BecomeMemberViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BecomeMember(BecomeMemberViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _memberService.EmailExistsAsync(model.Email))
            {
                ModelState.AddModelError(nameof(model.Email), "An account with this email already exists. Please sign in instead.");
                return View(model);
            }

            var tempPassword = "Welcome" + new Random().Next(1000, 9999) + "!";
            var (hash, salt) = PasswordHasher.HashPassword(tempPassword);

            var member = new Member
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                IsQuickSignup = true
            };

            await _memberService.CreateAsync(member);

            TempData["SuccessMessage"] =
                $"Welcome, {model.FullName}! Your temporary password is: {tempPassword} — please sign in and update your profile.";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? facilityType, string? location)
        {
            var model = new FacilitySearchViewModel
            {
                FacilityType = facilityType,
                Location = location,
                Results = await _facilityService.SearchAsync(facilityType, location),
                IsRestricted = true
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Inquiry() => View(new InquiryViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Inquiry(InquiryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await _inquiryService.CreateAsync(new Inquiry
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message
            });

            TempData["SuccessMessage"] = "Thanks for reaching out! Our team will get back to you soon.";
            return RedirectToAction("Index", "Home");
        }
    }
}