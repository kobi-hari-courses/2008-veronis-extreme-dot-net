using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public class ActionDisposable: DisposableBase
    {
        private Action _action;

        public ActionDisposable(Action action)
        {
            _action = action;
        }

        public override void OnDispose(bool isItSafeToFreManagedObjects)
        {
            base.OnDispose(isItSafeToFreManagedObjects);
            if (isItSafeToFreManagedObjects)
            {
                _action();
            }
        }
    }
}
