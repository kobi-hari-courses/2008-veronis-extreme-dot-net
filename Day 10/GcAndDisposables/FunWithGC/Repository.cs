using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public class Repository: DisposableBase
    {

        private List<string> _myData;
        private DataBase _myDataBase;

        public Repository()
        {
            _myDataBase = new DataBase();
            _myData = _myDataBase.GetData().ToList();
        }

        public override void OnDispose(bool isItSafeToFreManagedObjects)
        {
            base.OnDispose(isItSafeToFreManagedObjects);
            Console.WriteLine("Repository is being disposed");
            _myData.Clear();
            _myData = null;

            if (isItSafeToFreManagedObjects)
            {
                _myDataBase.Dispose();
            }

        }

        public IEnumerable<string> GetData()
        {
            Validate();
            return _myData.ToList();
        }
    }
}
