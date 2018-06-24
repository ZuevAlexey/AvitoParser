using System;
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

         var result = new AvitoParser().Get(settings).Result;
      }
   }
}
