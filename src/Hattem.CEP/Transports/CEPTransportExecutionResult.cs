using System;

namespace Hattem.CEP.Transports
{
    public enum CEPTransportExecutionResultType
    {
        Accept,
        Reject,
        Requeue
    }

    public readonly struct CEPTransportExecutionResult
    {
        public CEPTransportExecutionResultType Type { get; }

        public TimeSpan? RequeueDelay { get; }

        public CEPTransportExecutionResult(
            CEPTransportExecutionResultType type,
            TimeSpan? requeueDelay
        )
        {
            Type = type;
            RequeueDelay = requeueDelay;
        }

        public static CEPTransportExecutionResult Accept()
        {
            return new CEPTransportExecutionResult(CEPTransportExecutionResultType.Accept, requeueDelay: null);
        }

        public static CEPTransportExecutionResult Reject()
        {
            return new CEPTransportExecutionResult(CEPTransportExecutionResultType.Reject, requeueDelay: null);
        }

        public static CEPTransportExecutionResult Requeue(TimeSpan? delay = null)
        {
            if (delay?.TotalSeconds < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(delay), "Should be more than one second");
            }

            return new CEPTransportExecutionResult(CEPTransportExecutionResultType.Requeue, requeueDelay: delay);
        }
    }
}
