namespace Hattem.CEP.Jobs
{
    public interface IPersistentJob<TData> : IJob<TData>, IPersistentJob
        where TData : class
    {
    }
}
