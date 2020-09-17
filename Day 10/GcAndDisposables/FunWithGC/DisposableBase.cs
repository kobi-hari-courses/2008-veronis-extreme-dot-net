using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public class DisposableBase: INotifyDisposable
    {
        public bool IsDisposed { get; private set; }
        public event EventHandler Disposed;

        protected void Validate([CallerMemberName]string callerName = "")
        {
            if (IsDisposed) throw new ObjectDisposedException($"{GetType().Name}.{callerName}");
        }

        private void _dispose(bool isItSafeToFreManagedObjects)
        {
            if (IsDisposed) return;
            IsDisposed = true;

            OnDispose(isItSafeToFreManagedObjects);

            if (isItSafeToFreManagedObjects)
            {
                Disposed?.Invoke(this, EventArgs.Empty);
                Disposed = null;
            }

            GC.SuppressFinalize(this);
        }

        public virtual void OnDispose(bool isItSafeToFreManagedObjects)
        {
        }

        public void Dispose()
        {
            _dispose(true);
        }

        ~DisposableBase()
        {
            _dispose(false);
        }
    }
}
