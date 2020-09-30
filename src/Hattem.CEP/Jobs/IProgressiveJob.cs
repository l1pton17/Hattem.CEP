namespace Hattem.CEP.Jobs
{
    public interface IProgressiveJob
    {
        int Step { get; set; }

        long Progress { get; set; }

        long Total { get; set; }
    }
}
