namespace Hattem.CEP.Jobs
{
    public interface IRecurringJob : IPersistentJob
    {
        int SchedulingVersion { get; set; }
    }
}
