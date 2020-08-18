using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

            MyLinq();

        }


        public static void InsertData(IEnumerable<Car> cars)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());

            using(var db = new CarDb())
            {
                if (!db.Cars.Any())
                {
                    foreach (var car in cars)
                    {
                        db.Cars.Add(car);
                    }

                    db.SaveChanges();
                }
            }
        }

        public static void RunSomeQueries()
        {
            Func<int, bool> func = n => n > 20;
            Expression<Func<int, bool>> expr = n => n > 20;

            var compiled = expr.Compile();



            using (var db = new CarDb())
            {
                db.Database.Log = str =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(str);
                };

                //var porsches = db.Cars
                //    .Where(car => car.Make == "Porsche")
                //    .Where(car => car.CombinedFE > 20)
                //    .OrderBy(car => car.Model)
                //    .Take(10)
                //    .Select(car => new
                //    {
                //        Make = car.Make,
                //        Model = car.Model,
                //        Combined = car.CombinedFE
                //    });

                var porsches = (from car in db.Cars
                                where car.Make == "Porsche" && car.CombinedFE > 20
                                orderby car.Model
                                select new { Make = car.Make, Model = car.Model, Combined = car.CombinedFE })
                               .Take(10);


                foreach (var car in porsches)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{car.Make, -10}, {car.Model, -30}, {car.Combined}");
                }
            }
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

        public static void Group(IEnumerable<Car> cars)
        {
            //var groups = cars
            //    .GroupBy(car => car.Make)
            //    .Select(group => new
            //    {
            //        Make = group.Key, 
            //        Count = group.Count()
            //    });

            var groups = from car in cars
                         group car by car.Make into make
                         select new { Make = make.Key, Count = make.Count() };

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Make, -30}: {group.Count}");
            }
        }

        public static void SelectMany()
        {
            var words = new List<string> { "Hello", "World", "How", "Are", "You" };

            var query = words.Select(w => w.ToUpper())
                .SelectMany(word => word)
                .GroupBy(letter => letter)
                .Select(group => new { Letter = group.Key, Count = group.Count() });

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Letter}: {group.Count}");
            }

        }

        public static void Join(IEnumerable<Car> cars, IEnumerable<Manufacturer> manufacturers)
        {
            //var query = manufacturers.GroupJoin(cars, m => m.Name, c => c.Make, (m, c) => new
            //{
            //    Manufacturer = m,
            //    CarGroup = c
            //}).OrderBy(item => item.Manufacturer.Name);

            var query = from manufacturer in manufacturers
                        join car in cars on manufacturer.Name equals car.Make
                        into carGroup
                        orderby manufacturer.Name
                        select new { Manufacturer = manufacturer, CarGroup = carGroup };

            var allCars = query.SelectMany(group => group.CarGroup)
                .Where(car => car.Make == "Susita")
                .ToList();

            foreach (var item in query)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(item.Manufacturer.Name);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var car in item.CarGroup)
                {
                    Console.WriteLine($"{car.Model } {car.Displacement}");
                }
            }
        }

        public static void Structures(IEnumerable<Car> cars)
        {
            var list = cars.ToList();
            var array = cars.ToArray();
            var hashset = cars.ToHashSet();

            var carsByMake = cars.ToLookup(car => car.Make);

            var countOfMakes = cars
                .GroupBy(car => car.Make)
                .ToDictionary(group => group.Key, group => group.Count());

        }

        public static void MyLinq()
        {
            ILogable<string> logable = new Logable<string>();

            var log2 = from line in logable
                       where 20 > 30
                       select "hello";

            log2.Log();
        }
    }
}
