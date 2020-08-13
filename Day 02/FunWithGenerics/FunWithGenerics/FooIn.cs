using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    // Contra-variance
    public interface IFooIn<in T>
    {
    }

    public class FooIn<T> : IFooIn<T>
    {

    }
}
