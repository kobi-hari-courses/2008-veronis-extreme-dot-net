using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithLinq
{
    public interface ILogable<T>
    {
        List<string> Lines { get; }

        void Log();
    }

    public class Logable<T>: ILogable<T>
    {
        public List<string> Lines { get; private set; }

        public Logable()
        {
            Lines = new List<string>();
        }

        public Logable(string line)
        {
            Lines = new List<string> { line };
        }

        public Logable(IEnumerable<string> lines, string line)
        {
            Lines = lines.ToList();
            Lines.Add(line);
        }

        public Logable(Logable<T> source, string line)
        {
            Lines = source.Lines.ToList();
            Lines.Add(line);
        }

        public void Log()
        {
            foreach (var item in Lines)
            {
                Console.WriteLine(item);
            }
        }
    }
}
