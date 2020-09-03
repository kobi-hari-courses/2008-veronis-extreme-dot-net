using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithTasks
{
    public interface IRangeResolver
    {
        Task<(int start, int finish)> GetRange(CancellationToken ct);
    }
}
