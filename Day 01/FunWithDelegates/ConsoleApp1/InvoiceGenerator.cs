using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class InvoiceGenerator
    {
        private static int _invoiceNumber = 1000;

        public event EventHandler<int> InvoiceGenerated;

        public void GenerateInvoiceWithInterface(Order order, ITaxCalculator taxCalculator)
        {
            double total = 0.0;

            foreach (var product in order.Products)
            {
                double price = product.Units * product.Price;
                total += price;
                Console.WriteLine($"{product.Name} (price: {product.Price}, amount: {product.Units}, total:{price}");
            }

            Console.WriteLine("----------------------------------------------");
            var tax = taxCalculator.CalculateTax(order);
            Console.WriteLine($"Total Before Tax: {total}");
            Console.WriteLine($"Tax: {tax}");

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Total: {total + tax}");


        }

        public void GenerateInvoice(Order order, Func<Order, double> taxCalcMethod)
        {
            double total = 0.0;
            _invoiceNumber++;

            Console.WriteLine($"Invoice {_invoiceNumber}");
            Console.WriteLine("----------------------------------------------");

            foreach (var product in order.Products)
            {
                double price = product.Units * product.Price;
                total += price;
                Console.WriteLine($"{product.Name} (price: {product.Price}, amount: {product.Units}, total:{price}");
            }

            Console.WriteLine("----------------------------------------------");
            var tax = taxCalcMethod(order);
            Console.WriteLine($"Total Before Tax: {total}");
            Console.WriteLine($"Tax: {tax}");

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"Total: {total + tax}");

            // pattern 1 to avoid Null Reference Exception
            if (InvoiceGenerated != null)
            {
                InvoiceGenerated(this, _invoiceNumber);
            }

            // pattern 2 - from C# 6~7
            InvoiceGenerated?.Invoke(this, _invoiceNumber);


        }

    }
}
