using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public static class Disposables
    {
        public static T DisposedBy<T>(this T source, INotifyDisposable owner)
            where T: IDisposable
        {
            owner.Disposed += (s, e) =>
            {
                source.Dispose();
            };
            return source;
        }

        public static ActionDisposable Call(Action action)
        {
            return new ActionDisposable(action);
        }
    }
}
