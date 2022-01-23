using System;
using System.Data.Entity;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                {
                    //var cars = ProcessCars("fuel.csv");
                    //var manufacturers = ProcessManufacturers("manufacturers.csv");
                }
                {


                    //var query = cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
                    //    .OrderByDescending(c => c.Combined)
                    //    .Select(car => car);

                    //var result = cars.Any(c => c.Manufacturer == "Ford");
                    //Console.WriteLine(result);

                    //var query = 
                    //    cars.Join(manufacturers,
                    //            c => new { c.Manufacturer, c.Year },
                    //            m => new { Manufacturer =  m.Name, m.Year}, 
                    //            (c, m) => new
                    //            {
                    //                m.Headquarters,
                    //                c.Name,
                    //                c.Combined
                    //            })
                    //    .OrderByDescending(c => c.Combined)
                    //    .ThenBy(c => c.Name);

                    //foreach (var car in query.Take(10))
                    //{
                    //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
                    //}
                }
                {
                    //var query = cars.GroupBy(c => c.Manufacturer.ToUpper())
                    //                .OrderBy(g => g.Key);
                }
                {
                    //var query = manufacturers.GroupJoin(
                    //cars,
                    //m => m.Name,
                    //c => c.Manufacturer,
                    //(m, g) =>
                    //new
                    //{
                    //    Manufacturer = m,
                    //    Cars = g
                    //})
                    //.OrderBy(m => m.Manufacturer.Name);
                }
                {
                    //var query = manufacturers.GroupJoin(
                    //    cars,
                    //    m => m.Name,
                    //    c => c.Manufacturer,
                    //    (m, g) =>
                    //    new
                    //    {
                    //        Manufacturer = m,
                    //        Cars = g
                    //    })
                    //    .GroupBy(m => m.Manufacturer.Headquarters);
                }
                {
                    //foreach (var group in query)
                    //{
                    //    Console.WriteLine($"{group.Manufacturer.Name}:{group.Manufacturer.Headquarters}");
                    //    foreach (var item in group.Cars.OrderByDescending(c=>c.Combined).Take(2))
                    //    {
                    //        Console.WriteLine($"\t{item.Name}  :  {item.Combined}");
                    //    }
                    //}
                }
                {
                    //foreach (var result in query)
                    //{
                    //    Console.WriteLine($"{result.Name}");
                    //    Console.WriteLine($"\t Max: {result.Max}");
                    //}
                }
            }
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData();

        }

        private static void QueryData()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;
            var query = db.Cars
                .OrderByDescending(x => x.Combined)
                .ThenBy(x => x.Name);
            foreach (var item in query.Take(10))
            {
                Console.WriteLine($"{item.Name} : {item.Combined}");
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();
            if (!db.Cars.Any())
            {
                foreach (var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static List<Manufacturers> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columns = l.Split(',');
                    return new Manufacturers
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });
            return query.ToList();
        }

        private static List<Car> ProcessCars(string path)
        {
            return
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(line=> line.Length >1)
                    .Select(Car.ParseFromCsv)
                    .ToList();

        }
    }
}