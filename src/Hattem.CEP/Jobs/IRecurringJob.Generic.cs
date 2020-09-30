namespace Hattem.CEP.Jobs
{
    public interface IRecurringJob<TData> : IRecurringJob, IPersistentJob<TData>
        where TData : class
    {
    }
}
