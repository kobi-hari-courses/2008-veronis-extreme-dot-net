using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public struct Complex: IEquatable<Complex>
    {
        public double Imaginary { get; }

        public double Real { get; }

        public Complex(double i, double r)
        {
            Imaginary = i;
            Real = r;
        }

        public bool Equals(Complex other)
        {
            return (other.Real == Real) && (other.Imaginary == Imaginary);
        }

        public static bool operator ==(Complex c1, Complex c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Complex c1, Complex c2)
        {
            return !c1.Equals(c2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex)
            {
                return Equals((Complex)obj);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (Imaginary.GetHashCode() * 0x100000) + (Real.GetHashCode() * 0x1000);
        }




    }
}
