namespace MyCoolWebServer.ByTheCakeApplication.Data
{
    using ByTheCakeApplication.ViewModels;
    using Server.Common;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesData
    {
        public static string DefaultDataFilePath { get; } = "../../../ByTheCakeApplication\\Data\\database.csv";

        public void Add(string name, string price)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNullOrEmpty(price, nameof(price));

            var id = File.ReadAllLines(DefaultDataFilePath).Length;

            using (var streamWriter = new StreamWriter(DefaultDataFilePath, true))
            {
                streamWriter.WriteLine($"{id + 1},{name},{price}");
            }
        }

        public IEnumerable<Cake> GetCakes()
        {
            var allCakes = File.ReadAllLines(DefaultDataFilePath)
                               .Select(line => line.Split(','))
                               .Select(line => new Cake
                               {
                                   Id = int.Parse(line[0]),
                                   Name = line[1],
                                   Price = decimal.Parse(line[2])
                               })
                               .ToArray();

            return allCakes;
        }

        public Cake Find(int id) => this.GetCakes().FirstOrDefault(c => c.Id == id);
    }
}