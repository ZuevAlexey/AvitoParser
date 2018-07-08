using System;
using System.Threading;
using MyCompany.Avito.Parser.Data;
using MyCompany.Avito.Parser.Data.Enums;
using MyCompany.Avito.Parser.Parser;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.Avito.Parser {
   class Program {
      static void Main(string[] args) {
         var settings = new AvitoRequestSettings{
            Districts = District.Central | District.Sovetskiy | District.Privokzalniy | District.Proletarskiy | District.Zarechenskiy,
            Materials = Material.Block | Material.Kirpich | Material.Monolit,
            IsNotLast = true,
            FirstFloor = 2,
            HouseFloor = 5,
            PayMax = 3500000,
            PayMin = 3000000,
            MaxSquare = 80,
            MinSquare = 50
            };

         var parser = new AvitoParser();

         var avitoItems = parser.Get(settings).Result;

         var count = 0;
         foreach (var item in avitoItems) {
            AvitoFlatItem flatItem;
            try {
               flatItem = parser.GetFlat(item).Result;
            } catch (Exception ex){
               continue;
            }
            
            Console.WriteLine($"{++count}) {flatItem.Title}");
            Console.WriteLine(flatItem.Address);
            Console.WriteLine(flatItem.Price);
            Console.WriteLine(flatItem.SellerName);
            Console.WriteLine(flatItem.ContactName);
            foreach (var param in flatItem.Params) {
               Console.WriteLine($"{param.Key} : {param.Value}");
            }
            Console.WriteLine(flatItem.Desciption);
           
            Console.WriteLine();
            Thread.Sleep(1000);
         }

         Console.ReadLine();
      }
   }
}
