using System.Threading.Tasks;

namespace MyCompany.Avito.Parser.Data.Getter {
   /// <summary>
   /// Поставщик данных от авито
   /// </summary>
   internal interface IAvitoDataGetter {
      /// <summary>
      /// Возвращает данные по урлу от авито
      /// </summary>
      /// <param name="url"></param>
      /// <returns></returns>
      Task<string> Get(string url);
   }
}
