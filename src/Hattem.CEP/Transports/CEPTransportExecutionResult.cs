using System;

namespace Hattem.CEP.Transports
{
    public enum CEPTransportExecutionResultType
    {
        Ack,
        Nack,
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

        public static CEPTransportExecutionResult Ack()
        {
            return new CEPTransportExecutionResult(CEPTransportExecutionResultType.Ack, requeueDelay: null);
        }

        public static CEPTransportExecutionResult Nack()
        {
            return new CEPTransportExecutionResult(CEPTransportExecutionResultType.Nack, requeueDelay: null);
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
