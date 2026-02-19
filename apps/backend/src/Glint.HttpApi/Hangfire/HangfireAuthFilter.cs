using Hangfire.Dashboard;

namespace Glint.HttpApi.Hangfire;

public class HangfireAuthFilter : IDashboardAuthorizationFilter
{ 
    public bool Authorize(DashboardContext context) => true;
}