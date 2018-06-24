using System;

namespace MyCompany.Avito.Parser.Data.Enums {
   [Flags]
   internal enum Material {
      Kirpich = 1,
      Panel = 2,
      Block = 4,
      Monolit = 8,
      Derevo = 16
   }
}
