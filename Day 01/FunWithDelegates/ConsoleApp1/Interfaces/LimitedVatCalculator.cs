using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    public class LimitedVatCalculator : ITaxCalculator
    {
        public double CalculateTax(Order order)
        {
            var limit = Math.Min(order.GetTotalAmount(), 50.0);
            return limit * 0.3;
        }
    }
}
