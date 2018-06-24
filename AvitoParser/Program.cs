using System;
using System.Threading;
using MyCompany.Avito.Parser.Data;
using MyCompany.Avito.Parser.Data.Enums;
using MyCompany.Avito.Parser.Parser;

namespace MyCompany.Avito.Parser {
   class Program {
      static void Main(string[] args) {
         var settings = new AvitoRequestSettings{
            Districts = District.Central | District.Sovetskiy,
            Materials = Material.Block | Material.Kirpich | Material.Monolit,
            IsNotLast = true,
            FirstFloor = 2,
            HouseFloor = 5,
            PayMax = 3500000,
            PayMin = 3000000,
            MaxSquare = 80,
            MinSquare = 60
            };

         var parser = new AvitoParser();

         var avitoItems = parser.Get(settings).Result;

         foreach (var item in avitoItems) {
            var flatItem = parser.GetFlat(item).Result;
            Console.WriteLine(flatItem.Title);
            Console.WriteLine(flatItem.Address);
            Console.WriteLine(flatItem.Desciption);
            Console.WriteLine();
            Thread.Sleep(1000);
         }

         Console.ReadLine();
      }
   }
}
