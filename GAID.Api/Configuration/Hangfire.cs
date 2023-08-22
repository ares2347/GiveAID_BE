using GAID.Shared;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Newtonsoft.Json;

namespace GAID.Api.Configuration;

public static class Hangfire
{
    public static void AddHangfire(this IServiceCollection services)
    {
        services.AddHangfire(configuration =>
        {
            configuration.UseSqlServerStorage(AppSettings.Instance.ConnectionStrings.SqlServer,
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(1),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });
            configuration.UseSerializerSettings(new JsonSerializerSettings
                { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        });
        services.AddHangfireServer();
    }
    
    public static void RegisterRecurringJob()
    {
        
    }
}

public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        // Temporary enable Hangfire dashboard on development server
        return true;
    }
}