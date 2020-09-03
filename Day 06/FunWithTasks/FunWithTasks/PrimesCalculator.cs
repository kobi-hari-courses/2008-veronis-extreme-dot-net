using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace FunWithTasks
{
    public static class PrimesCalculator
    {
        private static Random _rand = new Random();
        private static Dictionary<(int start, int finish), List<int>> _cache = new Dictionary<(int start, int finish), List<int>>();

        private static bool _isPrime(this int number)
        {
            //var randomNumber = _rand.Next(150000);
            //if (randomNumber == 0) throw new AccessViolationException();

            for (int i = 2; i < number ; i++)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        private static IEnumerable<int> _getAllPrimes(int start, int finish, CancellationToken ct, IProgress<int> progress)
        {
            int lastReportedValue = -1;

            for (int i = start; i <= finish; i++)
            {
                //if (ct.IsCancellationRequested)
                //{
                //    throw new OperationCanceledException();
                //}

                if (progress != null)
                {
                    var progressValue = (int)(((double)i - start) / (finish - start) * 100);
                    if (progressValue > lastReportedValue)
                    {
                        lastReportedValue = progressValue;
                        progress.Report(progressValue);
                    }
                }

                ct.ThrowIfCancellationRequested();
                if (i._isPrime()) yield return i;
            }
        }

        private static IEnumerable<int> _getAllPrimesLinq(int start, int finish, CancellationToken ct)
        {
            return Enumerable.Range(start, finish - start + 1)
                      .AsParallel()
                      .AsOrdered()
                      .WithCancellation(ct)                      
                      .Where(i => _isPrime(i));
        }



        public static List<int> GetAllPrimes(int start, int finish, CancellationToken ct, IProgress<int> progress)
        {
            var res = _getAllPrimes(start, finish, ct, progress).ToList();
            _cache[(start, finish)] = res;

            return res;

            //return _getAllPrimesLinq(start, finish, ct).ToList();
        }

        public static Task<List<int>> GetAllPrimesAsync(
            int start, int finish, 
            CancellationToken ct = default, IProgress<int> progress = null)
        {
            if (_cache.ContainsKey((start: start, finish: finish)))
            {
                var res = _cache[(start, finish)];
                //return Task.FromResult(res);
                return Task.Delay(500).ContinueWith(t => res);

            }

            return Task.Factory.StartNew(() => GetAllPrimes(start, finish, ct, progress));
        }


    }
}
