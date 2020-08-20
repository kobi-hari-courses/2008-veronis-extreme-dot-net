using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public static class AgeChecker
    {
        public static string CheckAgeIsPositive(Person p)
        {
            if (p.Age < 0) return "Age is negative";

            return null;
        }
    }
}
