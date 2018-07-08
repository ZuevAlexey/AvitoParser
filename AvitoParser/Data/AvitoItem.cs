namespace MyCompany.Avito.Parser.Data {
   internal class AvitoItem {
      public string Url { get; set; }
      public string Title { get; set; }

      public override string ToString() {
         return Title;
      }
   }
}
