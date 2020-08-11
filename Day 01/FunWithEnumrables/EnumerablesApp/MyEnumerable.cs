using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerablesApp
{
    public class MyEnumerable : IEnumerable<int>
    {
        private class MyEnumerator : IEnumerator<int>
        {
            private int _current = 0;

            public int Current
            {
                get
                {
                    if (_current > 0) return _current;
                    throw new InvalidOperationException("Enumerator not initalized");
                }
            }

            object IEnumerator.Current { get { return Current; } }

            public bool MoveNext()
            {
                if (_current > 20) throw new ObjectDisposedException("Enumerator has completed");
                _current += 2;
                return _current <= 20;
            }

            public void Reset()
            {
                _current = 0;
            }

            public void Dispose()
            {
                _current = 22;
            }
        }


        public IEnumerator<int> GetEnumerator()
        {
            return new MyEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MyEnumerator();
        }


    }
}
