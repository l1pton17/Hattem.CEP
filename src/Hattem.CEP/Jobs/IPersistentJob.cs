using System;

namespace Hattem.CEP.Jobs
{
    public interface IPersistentJob
    {
        Guid Id { get; set; }

        Guid? ParentJobId { get; set; }

        Guid ConcurrencyId { get; set; }

        string CreatedBy { get; set; }

        string Name { get; set; }

        string Type { get; set; }

        JobStatus Status { get; set; }

        bool DeleteOnComplete { get; set; }

        DateTime? WhenExecuted { get; set; }

        DateTime WhenCreated { get; set; }

        DateTime WhenModified { get; set; }
    }
}
