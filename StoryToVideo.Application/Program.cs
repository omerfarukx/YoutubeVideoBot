using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StoryToVideo.Infrastructure.Data;
using StoryToVideo.Core.Interfaces;
using StoryToVideo.Infrastructure.Repositories;
using StoryToVideo.Infrastructure.Repositories.Users;
using StoryToVideo.Core.Interfaces.Services;
using StoryToVideo.Application.Services;
using StoryToVideo.Application.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Hangfire;
using Hangfire.Dashboard;
using StoryToVideo.Core.Entities;
using System.Threading.RateLimiting;
using StoryToVideo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger/OpenAPI configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoryToVideo API", Version = "v1" });
});

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<IAIService, AIService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Authentication için Identity ekleyelim
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Azure Blob Storage için
builder.Services.AddScoped<IBlobStorageService, AzureBlobStorageService>();

// Rate Limiting için
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.User?.Identity?.Name ?? context.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }));
});

// Video Processing Service
builder.Services.AddScoped<IVideoProcessingService, FFmpegVideoService>();

// Background Job Service (Hangfire)
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoryToVideo API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRateLimiter();
app.UseAuthentication();
app.MapControllers();

// Hangfire Dashboard
app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

app.Run();