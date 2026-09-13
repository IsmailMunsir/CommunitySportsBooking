using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using CommunitySportsBooking.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// ---- Configuration: bind MongoDbSettings from appsettings.json ----
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// ---- MongoDB context (singleton: MongoClient is thread-safe & pools connections) ----
builder.Services.AddSingleton<MongoDbContext>();

// ---- Application services ----
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();

// ---- MVC ----
builder.Services.AddControllersWithViews();

// ---- Cookie-based authentication for Members ----
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
        options.Cookie.Name = "CommunitySportsBooking.Auth";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// ---- Seed sample data on startup (facilities + demo member) ----
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    try
    {
        SeedData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogWarning(ex, "Could not seed database. Is MongoDB running and reachable?");
    }
}

// ---- HTTP pipeline ----
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();