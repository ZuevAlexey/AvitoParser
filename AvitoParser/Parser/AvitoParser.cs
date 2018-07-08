using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MyCompany.Avito.Parser.Data;
using MyCompany.Avito.Parser.Helpers;
using AngleSharp.Parser.Html;
using AngleSharp.Dom.Html;

namespace MyCompany.Avito.Parser.Parser {
   internal class AvitoParser : IAvitoParser {
      private IUrlHelper _urlHelper = new UrlHelper();

      private const string AVITO_ADDRESS_REGEX = "<span\\s+class=\"item-map-address\".+?<span>(?<district>.+?)<span.+?>(?<street>[\\s\\S]+?)<";

      public async Task<ICollection<AvitoItem>> Get(IAvitoRequestSettings settings) {
         var url = _urlHelper.GetUrl(settings);
         var document = GetDocument(url);
         var result = new List<AvitoItem>();

         foreach (var item in document.QuerySelectorAll("a.item-description-title-link")) {

            var avitoItem = new AvitoItem {
               Title = HttpUtility.HtmlDecode(item.InnerHtml.Trim('\n',' ')),
               Url = item.GetAttribute("href")
            };
            result.Add(avitoItem);

         }

         return result;
      }

      private IHtmlDocument GetDocument(string url) {
         var content = GetContent(url);
         var parser = new HtmlParser();
         return parser.Parse(content);
      }

      public async Task<AvitoFlatItem> GetFlat(AvitoItem flatItem) {
         var url = _urlHelper.GetUrl(flatItem);
         var result = new AvitoFlatItem();
         var document = GetDocument(url);


         result.Title = document.QuerySelector("span.title-info-title-text").InnerHtml.Trim('\n', ' ');
         result.Desciption = document.QuerySelector("div.item-description").TextContent.Trim('\n', ' ');
         result.Address = document.QuerySelector("span.item-map-address").TextContent.Replace("\n","").Trim('\n', ' ');

         var dirtyPrice = document.QuerySelector("span.js-item-price").TextContent.Trim('\n', ' ').Replace(" ","");
         result.Price = int.Parse(dirtyPrice);

         result.SellerName = document.QuerySelector("div.seller-info-name").TextContent.Trim('\n', ' ').Replace(" ","");

         var sellerInfoProp = document.QuerySelector("div.seller-info-prop.seller-info-prop_short_margin");
         result.ContactName = sellerInfoProp != null 
            ? sellerInfoProp.QuerySelector("div.seller-info-value").TextContent.Trim('\n', ' ').Replace(" ","")
            : "";

         foreach (var item in document.QuerySelector("ul.item-params-list").QuerySelectorAll("li.item-params-list-item")) {
            var values = item.TextContent.Trim('\n', ' ').Split(':');
            result.Params.Add(values[0].Trim(), values[1].Trim());
         } 
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
