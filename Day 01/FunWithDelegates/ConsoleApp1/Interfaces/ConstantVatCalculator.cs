using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    public class ConstantVatCalculator : ITaxCalculator
    {
        public double CalculateTax(Order order)
        {
            return 45.0;
        }
    }
}
