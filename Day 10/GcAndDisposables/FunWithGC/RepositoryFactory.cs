using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public class RepositoryFactory
    {
        private class Usage : ActionDisposable, IDisposableWrapper<Repository>
        {
            public Repository Value { get; private set; }

            public Usage(Action action, Repository value)
                :base(action)
            {
                Value = value;
            }
        }

        private Repository _myRepository = null;
        private int _usageCounter = 0;

        public IDisposableWrapper<Repository> GetRepository()
        {
            if (_usageCounter == 0)
            {
                _myRepository = new Repository();
            }

            _usageCounter++;
            return new Usage(() =>
            {
                _usageCounter--;
                if (_usageCounter == 0)
                {
                    _myRepository.Dispose();
                    _myRepository = null;
                }
            }, _myRepository);
        }


    }
}
