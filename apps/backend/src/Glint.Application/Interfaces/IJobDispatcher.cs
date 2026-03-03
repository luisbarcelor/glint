namespace Glint.Application.Interfaces;

public interface IJobDispatcher
{
    void EnqueueOptimization(Guid assetId);
}