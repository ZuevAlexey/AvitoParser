using System.Collections.Generic;
using System.Threading.Tasks;
using MyCompany.Avito.Parser.Data;
using MyCompany.Avito.Parser.Data.Getter;
using MyCompany.Avito.Parser.Helpers;

namespace MyCompany.Avito.Parser.Parser {
   internal class AvitoParser : IAvitoParser {
      private IUrlHelper _urlHelper = new UrlHelper();
      private IAvitoDataGetter _avitoDataGetter = new AvitoDataGetter();

      public async Task<string> Get(IAvitoRequestSettings settings) {
         var url = _urlHelper.GetUrl(settings);
         return await _avitoDataGetter.Get(url);
      }
   }
}
