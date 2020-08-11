using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public delegate int MyFunc(int i1, int i2);

    class Program
    {
        static private int f1(int x, int y)
        {
            Console.WriteLine($"f1: {x + y}");
            return 20;
        }

        private int f2(int a, int b)
        {
            Console.WriteLine($"f2: {a * b}");
            return 42;
        }

        static private double _calcTaxTheIsraeliWay(Order order)
        {
            return order.GetTotalAmount() * 0.17;
        }

        private double _calcTaxVeryLow(Order order)
        {
            return order.GetTotalAmount() * 0.1;
        }

        static private double _calcTwentyShequel(Order order)
        {
            return 20.0;
        }

        static void Main(string[] args)
        {
            var order = new Order
            {
                Products = new List<Product>
                {
                    new Product
                    {
                        Name = "Apples",
                        Price = 8,
                        Units = 3
                    },

                    new Product
                    {
                        Name = "Oranges",
                        Price = 5.3,
                        Units = 12
                    },

                    new Product
                    {
                        Name = "Milk",
                        Price = 2.5,
                        Units = 4
                    }
                }
            };

            var ig = new InvoiceGenerator();
            //ig.InvoiceGenerated += Ig_InvoiceGenerated;
            //ig.InvoiceGenerated += Ig_InvoiceGenerated2;

            var tc1 = new IsraelVatCalculator();
            var tc2 = new LimitedVatCalculator();
            var tc3 = new ConstantVatCalculator();

            var p = new Program();

            var d1 = new Func<Order, double>(_calcTwentyShequel);
            var d2 = new Func<Order, double>(p._calcTaxVeryLow);

            // C# 1: Explicit delegate creation
            ig.GenerateInvoice(order, new Func<Order, double>(_calcTwentyShequel));

            // implicit Delegate Creation
            ig.GenerateInvoice(order, _calcTwentyShequel);

            // C# 2: Annonimous Method
            ig.GenerateInvoice(order, delegate (Order o)
            {
                return o.GetTotalAmount() * 0.25;
            });

            // C# 3: Lambda Expression
            ig.GenerateInvoice(order, o => o.GetTotalAmount() * 0.2);


            Console.ReadLine();
        }

        private static void Ig_InvoiceGenerated(object sender, int args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invoide Generated ({args})");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        private static void Ig_InvoiceGenerated2(object sender, int args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Invoide Generated ({args})");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        static void Main1(string[] args)
        {
            var p = new Program();

            MyFunc myFunc = new MyFunc(f1);
            myFunc += new MyFunc(p.f2);

            var result = myFunc(5, 10);

            var delegates = myFunc.GetInvocationList();
        }


        private double __hidden_name_512(Order o)
        {
            return 20;
        }
    }
}
