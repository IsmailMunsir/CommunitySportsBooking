// Community Sports Facilities Booking — small UX helpers (no framework needed)
(function () {
    "use strict";

    // Auto-dismiss success/error banners after a few seconds
    document.querySelectorAll(".alert").forEach(function (el) {
        setTimeout(function () {
            el.style.transition = "opacity 0.4s ease";
            el.style.opacity = "0";
            setTimeout(function () { el.remove(); }, 400);
        }, 6000);
    });

    // Prevent picking a booking date in the past
    var dateInputs = document.querySelectorAll('input[type="date"]');
    dateInputs.forEach(function (input) {
        var today = new Date().toISOString().split("T")[0];
        if (!input.getAttribute("min")) {
            input.setAttribute("min", today);
        }
    });

    // Simple client-side guard: end time must be after start time on booking form
    var startTime = document.querySelector('input[name="StartTime"]');
    var endTime = document.querySelector('input[name="EndTime"]');
    if (startTime && endTime) {
        var validate = function () {
            if (startTime.value && endTime.value && endTime.value <= startTime.value) {
                endTime.setCustomValidity("End time must be after start time.");
            } else {
                endTime.setCustomValidity("");
            }
        };
        startTime.addEventListener("change", validate);
        endTime.addEventListener("change", validate);
    }
})();