using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    // Covariance
    public interface IFooOut<out T>
    {
    }

    public class FooOut<T> : IFooOut<T>
    {

    }
}
