using System;
using System.Collections.Generic;
using MyCompany.Avito.Parser.Data.Enums;

namespace MyCompany.Avito.Parser.Data {
   internal interface IAvitoRequestSettings {

      IEnumerable<string> Segments {get; }

      string DefaultParameter {get; }

      District? Districts { get; set;}

      Material? Materials { get; set;}

      int? FirstFloor {get; set;}

      int? HouseFloor {get; set;}

      int? MinSquare { get;set;}

      int? MaxSquare { get;set;}

      int? PayMax { get; set;}

      int? PayMin { get; set;}

      bool IsNotLast { get; set;}

      bool HasParameters { get;}
   }
}
