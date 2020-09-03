using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithTasks
{
    public class FixedRange : IRangeResolver
    {
        public Task<(int start, int finish)> GetRange(CancellationToken ct)
        {
            return Task.FromResult((2, 200000));
        }
    }
}
