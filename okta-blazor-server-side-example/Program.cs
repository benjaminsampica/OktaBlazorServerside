using AspNet.Security.OAuth.Okta;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using okta_blazor_server_side_example.Data;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OktaAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOkta(options =>
    {
        options.ClientId = builder.Configuration.GetValue<string>("Okta:ClientId");
        options.ClientSecret = builder.Configuration.GetValue<string>("Okta:ClientSecret");
        options.Domain = builder.Configuration.GetValue<string>("Okta:OktaDomain");

        options.Scope.Add("groups");
        options.ClaimActions.MapJsonKey(ClaimTypes.Role, "groups");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
