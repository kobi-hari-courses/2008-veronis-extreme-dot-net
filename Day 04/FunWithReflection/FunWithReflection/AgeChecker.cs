using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public static class AgeChecker
    {
        [Check]
        public static string CheckAgeIsPositive(Person p)
        {
            if (p.Age < 0) return "Age is negative";

            return null;
        }

        [Check]
        public static string CheckAgeIsBelow120(Person p)
        {
            if (p.Age > 120) return "Age is above 120";

            return null;
        }

        [Check]
        public static string CheckIntegerIsEven(int i)
        {
            if (i % 2 == 1) return "Number is Odd";
            return null;
        }

        [Check]
        public static string CheckDoubleIsNegative(double d)
        {
            if (d > 0) return "Number is Positive";
            return null;
        }

    }
}
