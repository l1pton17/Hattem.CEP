namespace Hattem.CEP.Jobs
{
    public interface IProgressiveJob<TData> : IJob<TData>, IProgressiveJob
        where TData : class
    {

    }
}
