namespace Hattem.CEP.Jobs
{
    public interface IJob<TData> : IJob
        where TData : class
    {
        TData Data { get; set; }
    }
}
