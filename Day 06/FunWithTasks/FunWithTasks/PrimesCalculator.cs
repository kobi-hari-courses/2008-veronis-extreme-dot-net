using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace FunWithTasks
{
    public static class PrimesCalculator
    {
        private static bool _isPrime(this int number)
        {
            for (int i = 2; i < number ; i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        private static IEnumerable<int> _getAllPrimes(int start, int finish)
        {
            for (int i = start; i <= finish; i++)
            {
                if (i._isPrime()) yield return i;
            }
        }

        public static List<int> GetAllPrimes(int start, int finish)
        {
            return _getAllPrimes(start, finish).ToList();
        }

        public static Task<List<int>> GetAllPrimesAsync(int start, int finish)
        {
            return Task.Factory.StartNew(() => GetAllPrimes(start, finish));
        }


    }
}
