using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Sport_Match.Data;
using Sport_Match.Repositories;
using Sport_Match.Services;
<<<<<<< Updated upstream
=======
using Sport_Match.Services.Auth;
using Sport_Match.Services.Registration;
using Sport_Match.Services.Sorting;
>>>>>>> Stashed changes
using System.Net.Http;
using Sport_Match.Services.Auth;
using Sport_Match.Services.Security;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Google";
})
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
})
.AddOAuth("Google", options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

    options.AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/auth";
    options.TokenEndpoint = "https://oauth2.googleapis.com/token";
    options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";

    options.CallbackPath = "/signin-google";

    options.Scope.Add("email");
    options.Scope.Add("profile");
    options.Scope.Add("https://www.googleapis.com/auth/calendar");

    options.SaveTokens = true;

    options.Events.OnRedirectToAuthorizationEndpoint = context =>
    {
        context.Response.Redirect(context.RedirectUri + "&prompt=select_account");
        return Task.CompletedTask;
    };

    options.ClaimActions.MapJsonKey("picture", "picture");
    options.ClaimActions.MapJsonKey("locale", "locale");

    options.Events.OnCreatingTicket = async context =>
    {
        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);

        var response = await context.Backchannel.SendAsync(request);

        var json = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        context.RunClaimActions(json.RootElement);
    };
});



builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<GoogleCalendarService>();
builder.Services.AddScoped<IAuthService, CookieAuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasherAdapter>();
builder.Services.AddScoped<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();


builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IUserRegistrationService>(sp =>
    sp.GetRequiredService<UserService>());

builder.Services.AddScoped<IUserAuthenticationService>(sp =>
    sp.GetRequiredService<UserService>());

builder.Services.AddScoped<IClaimsPrincipalFactory, ClaimsPrincipalFactory>();
builder.Services.AddScoped<IAuthService, CookieAuthService>();

builder.Services.AddScoped<IRegistrationStrategy, CapacityWaitlistStrategy>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

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
