using System.Threading.Tasks;
using MyCompany.Avito.Parser.Data;

namespace MyCompany.Avito.Parser.Parser {
   interface IAvitoParser {
      Task<string> Get(IAvitoRequestSettings settings);
   }
}
