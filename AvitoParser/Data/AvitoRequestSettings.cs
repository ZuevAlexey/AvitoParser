using System;
using System.Collections.Generic;
using MyCompany.Avito.Parser.Data.Enums;

namespace MyCompany.Avito.Parser.Data {
   internal class AvitoRequestSettings : IAvitoRequestSettings {
      private const string IS_NOT_LAST_SEGMENT = "ne_posledniy";

      private List<string> _defaultSegments = new List<string> { 
            "tula"
            ,"kvartiry"
            ,"prodam"
            ,"3-komnatnye"
            ,"vtorichka" };

      public IEnumerable<string> Segments {
         get {
            var result = new List<string>();
            result.AddRange(_defaultSegments);
            if (IsNotLast) {
               result.Add(IS_NOT_LAST_SEGMENT);
            }
            return result;
         }
      }

      public string DefaultParameter => "s_trg=3";

      public District? Districts {get; set;}

      public bool HasParameters {
         get {
            return Districts.HasValue
               || Materials.HasValue 
               || FirstFloor.HasValue
               || HouseFloor.HasValue
               || PayMax.HasValue
               || PayMin.HasValue
               || MinSquare.HasValue
               || MaxSquare.HasValue;
         }
      }

      public bool IsNotLast { get;set; } = true;

      public Material? Materials { get; set;}

      public int? FirstFloor { get; set; }

      public int? HouseFloor { get; set; }

      public int? PayMax { get; set; }

      public int? PayMin { get; set; }

      public int? MinSquare { get; set; }

      public int? MaxSquare { get; set; }
   }
}
