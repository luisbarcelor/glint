using Glint.Application.Interfaces;
using Glint.Application.Services;
using Hangfire;

namespace Glint.Infrastructure.BackgroundJobs;

public class HangfireJobDispatcher : IJobDispatcher
{
    private readonly IBackgroundJobClient _jobClient;

    public HangfireJobDispatcher(IBackgroundJobClient jobClient)
    {
        _jobClient = jobClient;
    }

    public void EnqueueOptimization(Guid assetId)
    {
        _jobClient.Enqueue<IAssetService>(x =>
            x.OptimizeAssetAsync(assetId, CancellationToken.None));
    }
}