using Hangfire.Dashboard;

namespace StoryToVideo.Infrastructure.Services;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        // TODO: Add proper authorization logic
        return true;
    }
} 