using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public static class SomeKindOfObservable
    {
        private static BehaviorSubject<int> _data = new BehaviorSubject<int>(0);

        public static IObservable<int> GetData()
        {
            return _data.AsObservable();
        }
    }
}
