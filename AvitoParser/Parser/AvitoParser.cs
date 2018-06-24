using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using MyCompany.Avito.Parser.Data;
using MyCompany.Avito.Parser.Helpers;

namespace MyCompany.Avito.Parser.Parser {
   internal class AvitoParser : IAvitoParser {
      private IUrlHelper _urlHelper = new UrlHelper();

      private const string AVITO_ITEM_REGEX = "<a\\s+class=\"item-description-title-link\"\\s+href=\"(?<url>.+?)\"\\s+title=\"(?<title>.+?)\"";
      private const string AVITO_TITLE_REGEX = "<span\\s+class=\"title-info-title-text\">(?<title>.+?)</span>";
      private const string AVITO_ADDRESS_REGEX = "<span\\s+class=\"item-map-address\".+?<span>(?<district>.+?)<span.+?>(?<street>[\\s\\S]+?)<";
      private const string AVITO_DESCRIPTION_REGEX = "<div\\s+class=\"item-description-text\"[\\s\\S]*?<p>(?<description>[\\s\\S]*?)<";

      public async Task<ICollection<AvitoItem>> Get(IAvitoRequestSettings settings) {
         var url = _urlHelper.GetUrl(settings);
         var content =  GetContent(url);
         var regex = new Regex(AVITO_ITEM_REGEX);
         var result = new List<AvitoItem>();
         var match = regex.Match(content);

         while (match.Success) {
            var avitoItem = new AvitoItem {
               Title = HttpUtility.HtmlDecode(match.Groups["title"].Value),
               Url = match.Groups["url"].Value
            };
            result.Add(avitoItem);

            match = match.NextMatch();
         }

         return result;
      }

      public async Task<AvitoFlatItem> GetFlat(AvitoItem item) {
         var url = _urlHelper.GetUrl(item);
         var content = GetContent(url);
         var result = new AvitoFlatItem();
         result.Title = new Regex(AVITO_TITLE_REGEX).Match(content).Groups["title"].Value;
         result.Desciption = new Regex(AVITO_DESCRIPTION_REGEX).Match(content).Groups["description"].Value;
         var addressMatch = new Regex(AVITO_ADDRESS_REGEX).Match(content);
         result.Address = addressMatch.Groups["district"].Value +" " + addressMatch.Groups["street"].Value.Trim(' ', '\n');
         return result;
      }

       private string GetContent(string url) {
         var client = new HttpClient();
         try {
            var response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
         } catch {
            return "";
         } 
      }
   }
}
