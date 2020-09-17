using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public class ViewModel: DisposableBase
    {
        private Repository _repository;

        public ViewModel()
        {
            _repository = new Repository().DisposedBy(this);

            SomeKindOfObservable.GetData().Subscribe(val =>
            {
                Console.WriteLine(val);
            }).DisposedBy(this);

        }
    }
}
