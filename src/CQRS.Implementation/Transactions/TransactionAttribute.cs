using System;
using System.Data;

namespace CQRS.Implementation.Transactions
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TransactionAttribute : Attribute
    {        
        public IsolationLevel TransactionType { get; }

        public TransactionAttribute(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            TransactionType = isolationLevel;
        }
    }
}
