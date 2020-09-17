using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public interface INotifyDisposable: IDisposable
    {
        bool IsDisposed { get; }
        event EventHandler Disposed;
    }
}
