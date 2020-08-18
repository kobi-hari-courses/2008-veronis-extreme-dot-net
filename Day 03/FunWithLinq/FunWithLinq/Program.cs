using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace FunWithLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = Reader.CarsFromCsv("Data/fuel.csv");
            var manufacturers = Reader.ManufacturersFromCsv("Data/manufacturers.csv");

            Aggregate(cars, manufacturers);
        }

        public static void MostEfficientCars(IEnumerable<Car> cars)
        {
            //var query = cars.OrderByDescending(c => c.CombinedFE)
            //                .ThenBy(c => c.Make)
            //                .ThenBy(c => c.Model)
            //                .Take(10);

            IEnumerable<Car> query = from car in cars
                        orderby car.CombinedFE descending, car.Make, car.Model
                        select car;

            query = query.Take(10);

            foreach (var car in query)
            {
                Console.WriteLine($"{car.Make, -30} {car.Model, -30} {car.CombinedFE, -30}");
            }
        }

        public static void Aggregate(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            var porsches = cars.Where(c => c.Make == "Porsche");

            //var sumOfCombinedFe = cars.Sum(car => car.CombinedFE);
            //var count = cars.Count();
            //var avgCombinedFe = sumOfCombinedFe / count;

            //var numbers = new List<int> { 1, 3, 9, 24, 35, 20 };
            //numbers.Aggregate((i1, i2) => Math.Max(i1, i2));

            var accumulated = porsches.Aggregate(
                new
                {
                    Count = 0,
                    Sum = 0
                }, (accumulator, car) => new
                {
                    Count = accumulator.Count + 1,
                    Sum = accumulator.Sum + car.CombinedFE
                },
                accumulator => (double)accumulator.Sum / accumulator.Count);
        }
    }
}
