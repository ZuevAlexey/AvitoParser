using System;

namespace MyCompany.Avito.Parser.Data.Enums {
   [Flags]
   internal enum District {
      Zarechenskiy = 1,
      Privokzalniy = 2,
      Proletarskiy = 4,
      Sovetskiy = 8,
      Central = 16
   }
}
