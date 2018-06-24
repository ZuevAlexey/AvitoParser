using MyCompany.Avito.Parser.Data;

namespace MyCompany.Avito.Parser.Helpers {
   /// <summary>
   /// Интерфейс помощника для работы с урлами для Авито
   /// </summary>
   internal interface IUrlHelper {
      string GetUrl(IAvitoRequestSettings settings);
   }
}
