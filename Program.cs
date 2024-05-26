using MatutesAuctionHouse.Hubs;
using MatutesAuctionHouse.Models;
using MatutesAuctionHouse.Models.Common;
using MatutesAuctionHouse.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

var builder = WebApplication.CreateBuilder(args);

    string corspol = "_acceptedOrigins";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corspol,
                      policy =>
                      {
                        policy.WithOrigins("https://localhost:44442") // URL de tu aplicación Angular
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
});
builder.Services.AddControllers();
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
builder.Services.AddScoped<AuctionService>();
builder.Services.AddSignalR();

// JWT
var appSettings = appSettingsSection.Get<AppSettings>();
var jwtKey = Encoding.ASCII.GetBytes(appSettings.JWTSecret);
builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("database")));
builder.Services.AddSignalR();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(corspol);
app.UseWebSockets();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<AuctionHub>("/auctionHub");
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
