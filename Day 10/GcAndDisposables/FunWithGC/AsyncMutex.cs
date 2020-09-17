using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public class AsyncMutex
    {
        private IDisposable _currentToken = null;
        private Queue<TaskCompletionSource<IDisposable>> _queue = new Queue<TaskCompletionSource<IDisposable>>();
        private object _mutex = new object();


        public Task<IDisposable> Lock()
        {
            lock(_mutex)
            {
                if (_currentToken == null)
                {
                    var token = Disposables.Call(Release);
                    _currentToken = token;
                    return Task.FromResult<IDisposable>(token);
                } else
                {
                    var tcs = new TaskCompletionSource<IDisposable>();
                    _queue.Enqueue(tcs);
                    return tcs.Task;
                }
            }
        }

        private void Release()
        {
            lock(_mutex)
            {
                if (_queue.Any())
                {
                    var tcs = _queue.Dequeue();
                    var token = Disposables.Call(Release);
                    tcs.SetResult(token);
                } else
                {
                    _currentToken = null;
                }

            }

        }


    }
}
