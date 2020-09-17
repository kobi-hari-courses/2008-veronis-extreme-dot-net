using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public class DataBase: DisposableBase
    {
        public IEnumerable<string> GetData()
        {
            Validate();
            yield return "Hello";
            yield return "World";
        }

        public override void OnDispose(bool isItSafeToFreManagedObjects)
        {
            Console.WriteLine("Database is being disposed");
        }
    }
}
