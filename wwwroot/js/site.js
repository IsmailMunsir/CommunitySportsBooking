// Community Sports Facilities Booking — motion, mobile nav, loading states
(function () {
    "use strict";

    /* ---------- Top loading bar ---------- */
    var bar = document.getElementById("page-loading-bar");
    function showBar() {
        if (!bar) return;
        bar.classList.remove("done");
        bar.style.width = "0%";
        requestAnimationFrame(function () { bar.style.width = "75%"; });
    }
    function finishBar() {
        if (!bar) return;
        bar.style.width = "100%";
        setTimeout(function () { bar.classList.add("done"); }, 250);
    }
    window.addEventListener("load", finishBar);
    document.addEventListener("click", function (e) {
        var link = e.target.closest("a[href]");
        if (!link) return;
        var href = link.getAttribute("href");
        if (!href || href.startsWith("#") || link.target === "_blank") return;
        showBar();
    });
    document.addEventListener("submit", function () { showBar(); });

    /* ---------- Auto-dismiss success/error banners ---------- */
    document.querySelectorAll(".alert").forEach(function (el) {
        setTimeout(function () {
            el.style.transition = "opacity 0.4s ease";
            el.style.opacity = "0";
            setTimeout(function () { el.remove(); }, 400);
        }, 6000);
    });

    /* ---------- Prevent picking a booking date in the past ---------- */
    document.querySelectorAll('input[type="date"]').forEach(function (input) {
        var today = new Date().toISOString().split("T")[0];
        if (!input.getAttribute("min")) input.setAttribute("min", today);
    });

    /* ---------- Booking form: end time must be after start time ---------- */
    var startTime = document.querySelector('input[name="StartTime"]');
    var endTime = document.querySelector('input[name="EndTime"]');
    if (startTime && endTime) {
        var validateTimes = function () {
            if (startTime.value && endTime.value && endTime.value <= startTime.value) {
                endTime.setCustomValidity("End time must be after start time.");
            } else {
                endTime.setCustomValidity("");
            }
        };
        startTime.addEventListener("change", validateTimes);
        endTime.addEventListener("change", validateTimes);
    }

    /* ---------- Mobile hamburger nav ---------- */
    var toggle = document.getElementById("navToggle");
    var navLinks = document.getElementById("navLinks");
    if (toggle && navLinks) {
        toggle.addEventListener("click", function () {
            var isOpen = navLinks.classList.toggle("nav-open");
            toggle.classList.toggle("open", isOpen);
            toggle.setAttribute("aria-expanded", isOpen ? "true" : "false");
        });
        navLinks.querySelectorAll("a").forEach(function (a) {
            a.addEventListener("click", function () {
                navLinks.classList.remove("nav-open");
                toggle.classList.remove("open");
            });
        });
    }

    /* ---------- Scroll-reveal for sections and cards ---------- */
    var revealTargets = document.querySelectorAll(".section, .form-card, .hero-card, .card");
    if ("IntersectionObserver" in window && revealTargets.length) {
        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add("in-view");
                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1, rootMargin: "0px 0px -40px 0px" });

        revealTargets.forEach(function (el, i) {
            el.classList.add("reveal-init");
            el.style.transitionDelay = Math.min(i % 6, 5) * 0.06 + "s";
            observer.observe(el);
        });
    }

    /* ---------- Button loading state on form submit ---------- */
    document.querySelectorAll("form").forEach(function (form) {
        form.addEventListener("submit", function () {
            if (!form.checkValidity || form.checkValidity()) {
                var submitBtn = form.querySelector('button[type="submit"]');
                if (submitBtn && !submitBtn.classList.contains("btn-loading")) {
                    submitBtn.classList.add("btn-loading");
                }
            }
        });
    });
})();