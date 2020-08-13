using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    class Program
    {
        static void Main(string[] args)
        {

        }


        static void StaticGenericDemo()
        {
            var t = typeof(List<int>);// closed 

            var t2 = typeof(List<>); // open


            var fint = new Foo<int>(15);
            var fdouble = new Foo<Double>(12.5);
            var fstring = new Foo<string>("Hello");
            var fstring2 = new Foo<string>("Ma Hamatsav");

            Console.WriteLine(fstring2.OrdinalIndex);

            Console.ReadLine();

        }

        static void CovarianceDemo1()
        {
            var ls = new List<Sub>();
            var lb = new List<Base>();


            var b = new Base();
            var s = new Sub();

            ls.Add(s);
            lb.Add(b);

            // ls.Add(b); error
            lb.Add(s);

            DoSomething(ls);
            // DoSomething2(ls); error

            var fooin = new FooIn<Sub>();
            var fooout = new FooOut<Sub>();

            IFooIn<Sub> returnValueIn = GetFooIn();
            //IFooOut<Sub> returnValueOut = GetFooOut();

            // GetFooIn(fooin); illeagal due to covariance rules
            GetFooOut(fooout); // legal due to covariance rules
        }

        static void EquatableDemo()
        {
            var c1 = new Complex(10, 5);
            var c2 = new Complex(20, 10);

            if (c1.Equals(c2))
            {
                Console.WriteLine("Equals");
            }

            if (c1 == c2)
            {

            }
        }

        static void CovarianceDemo2()
        {
            // In-variant
            //IFoo<Base> ifbase = new Foo<Sub>();
            //IFoo<Sub> ifsub = new Foo<Base>();

            // Co-variance
            IFooOut<Base> ifbase = new FooOut<Sub>();
            //IFooOut<Sub> ifsub = new FooOut<Base>();

            // Contra-variance
            //IFooIn<Base> infbase = new FooIn<Sub>();
            IFooIn<Sub> infsub = new FooIn<Base>();

            IEnumerable<int> listInt = new List<int> { 1, 2, 3 };
            IEnumerable<object> someObjects = (IEnumerable<object>)listInt;

            Func<Base, Sub> func = n => new Sub();
            Action<int> action = n => Console.WriteLine(n);

            GetFunc(func);

        }

        static void ConstraintsDemo()
        {
            var c1 = new Complex(10, 5);
            var c2 = new Complex(20, 10);

            Actions.LogEquality<Complex>(c1, c2);

            var s1 = new Sub();
            var s2 = new Sub();
            //Actions.LogEquality<Sub>(s1, s2);

            var instance = new NoDefaultCtor(12);

            var s = Actions.Factory<Sub>();
            //var nd = Actions.Factory<NoDefaultCtor>();
        }

        static void GetFunc(Func<Sub, Base> f)
        {
            f(new Sub());
        }


        static IFooIn<Base> GetFooIn(IFooIn<Base> f = null)
        {
            return null;
        }

        static IFooOut<Base> GetFooOut(IFooOut<Base> f = null)
        {
            return null;
        }


        static void DoSomething(IEnumerable<Base> someItems)
        {
            foreach (var item in someItems)
            {
                Console.WriteLine(item.A);
            }
        }

        static void DoSomething2(List<Base> someItems)
        {
            foreach (var item in someItems)
            {
                Console.WriteLine(item.A);
            }

            someItems.Add(new Base());
        }

    }
}
