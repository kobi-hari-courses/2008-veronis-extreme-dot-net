using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithLinq
{
    public static class Reader
    {
        public static IEnumerable<Car> CarsFromCsv(string path)
        {
            return File.ReadAllLines(path)
                              .Skip(1)
                              .Where(line => !string.IsNullOrWhiteSpace(line))
                              .ToCars();
        }

        public static IEnumerable<Manufacturer> ManufacturersFromCsv(string path)
        {
            return File.ReadAllLines(path)
                       .Where(line => !string.IsNullOrWhiteSpace(line))
                       .Select(line => line.ToManufacturer());
        }

        public static IEnumerable<Car> ToCars(this IEnumerable<string> source)
        {
            foreach (var item in source)
            {
                yield return item.ToCar();
            }
        }

        public static Car ToCar(this string line)
        {
            var columns = line.Split(',');
            return new Car
            {
                Year = int.Parse(columns[0]),
                Make = columns[1],
                Model = columns[2],
                Displacement = double.Parse(columns[3]),
                Cylinders = int.Parse(columns[4]),
                CityFE = int.Parse(columns[5]),
                HighwayFE = int.Parse(columns[6]),
                CombinedFE = int.Parse(columns[7])
            };
        }

        public static Manufacturer ToManufacturer(this string line)
        {
            var columns = line.Split(',');
            return new Manufacturer
            {
                Name = columns[0],
                Headquarters = columns[1],
                Year = int.Parse(columns[2]),
            };
        }

    }
}
