using System;

using LittleLogBook.Data.Contracts;

namespace LittleLogBook.Data.Managers
{
    public abstract class ManagerBase : IDisposable
    {
        private bool _isDisposed;

        public virtual IUser CurrentUser { get; private set; }

        internal ManagerBase(IUser user)
        {
            CurrentUser = user;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    CurrentUser = null;
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }
    }
}
