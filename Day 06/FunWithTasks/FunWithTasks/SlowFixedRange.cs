using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithTasks
{
    public class SlowFixedRange : IRangeResolver
    {
        public async Task<(int start, int finish)> GetRange(CancellationToken ct)
        {
            await Task.Delay(2000, ct);
            return (2, 200000);
        }
    }
}
