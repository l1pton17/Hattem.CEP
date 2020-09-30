namespace Hattem.CEP.Jobs
{
    /// <summary>
    /// Job status
    /// </summary>
    public enum JobStatus
    {
        Scheduled = 0,
        Processing = 1,
        Completed = 2,
        Cancelled = 3,
        Failed = 4
    }
}
