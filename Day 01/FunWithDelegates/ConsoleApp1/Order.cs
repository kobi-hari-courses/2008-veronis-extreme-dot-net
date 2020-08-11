using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Order
    {
        public List<Product> Products { get; set; }

        public double GetTotalAmount()
        {
            var total = 0.0;

            foreach (var product in Products)
            {
                total += (product.Price * product.Units);
            }

            return total;
        }
    }
}
