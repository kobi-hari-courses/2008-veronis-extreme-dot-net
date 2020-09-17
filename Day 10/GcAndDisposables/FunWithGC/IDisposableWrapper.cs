using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public interface IDisposableWrapper<T>: IDisposable
    {
        T Value { get; }
    }
}
