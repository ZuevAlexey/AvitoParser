using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Avito.Parser.Data;

namespace MyCompany.Avito.Parser.Parser {
   interface IAvitoParser {
      Task<ICollection<AvitoItem>> Get(IAvitoRequestSettings settings);

      Task<AvitoFlatItem> GetFlat(AvitoItem item);
   }
}
