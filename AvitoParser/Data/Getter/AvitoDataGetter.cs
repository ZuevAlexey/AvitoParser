using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.Avito.Parser.Data.Getter {
   /// <summary>
   /// Поставщик данных от авито
   /// </summary>
   internal class AvitoDataGetter : IAvitoDataGetter {
      /// <summary>
      /// Получить данные от авито по урлу
      /// </summary>
      /// <param name="url">Урл для запроса</param>
      /// <returns>Данные</returns>
      public async Task<string> Get(string url) {
         var client = new HttpClient();
         var response = await client.GetAsync(url);
         return await response.Content.ReadAsStringAsync();
      }
   }
}
