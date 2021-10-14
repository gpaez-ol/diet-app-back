using System;

namespace AlgoFit.Errors
{
    public class AlgoFitError : Exception
    {
        public readonly int Status = 500;
        public readonly string MessageType = "application/json";

        public AlgoFitError(int status, string message) : base(message)
        {
            Status = status;
        }

        public AlgoFitError(int status, string message, string messageType) : base(message)
        {
            Status = status;
            MessageType = messageType;
        }

        public virtual object GetErrorObject()
        {
            return Message;
        }

        public static AlgoFitError TransactionError = new AlgoFitError(500, "Error while saving all transactions to the database");
    }
}
