using Shared.Interfaces;
using System;
using System.Transactions;

namespace Shared.Services
{
    public class TransactionService : IDisposable
    {
        private TransactionScope _scope;

        public TransactionService()
        {
            var options = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.DefaultTimeout
            };
            _scope = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public void Complete()
        {
            _scope.Complete();
        }

        public void Dispose()
        {
            _scope.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
