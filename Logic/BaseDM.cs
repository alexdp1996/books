using Shared.Interfaces;
using System;

namespace Logic
{
    public class BaseDM : IDisposable
    {
        protected IFactory Factory { get; }

        public BaseDM(IFactory factory)
        {
            Factory = factory;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
