using System;
using System.Collections.Generic;
using System.Text;
using MyCompany.Avito.Parser.Data;
using MyCompany.Avito.Parser.Data.Enums;

namespace MyCompany.Avito.Parser.Helpers {
   /// <summary>
   /// Помощник для работы с урлами авито
   /// </summary>
   internal class UrlHelper : IUrlHelper {
      private const char PARAMETER_DEVIDER = '&';

      private const string AVITO_MAIN_URL = "https://www.avito.ru";

      private Dictionary<District, string> _districtParameters = new Dictionary<District, string> {
        { District.Zarechenskiy, "281"},
        { District.Privokzalniy, "282"},
        { District.Proletarskiy, "283"},
        { District.Sovetskiy, "284"},
        { District.Central, "285"}
      };

      private Dictionary<Material, string> _materialParameters = new Dictionary<Material, string> {
        { Material.Kirpich, "5244"},
        { Material.Panel, "5245"},
        { Material.Block, "5246"},
        { Material.Monolit, "5247"},
        { Material.Derevo, "5248"}
      };

      private Dictionary<int, string> _squareFactors = new Dictionary<int, string> {
         { 10, "0"},
         { 15, "13984"},
         { 20, "13985"},
         { 25, "13986"},
         { 30, "13987"},
         { 40, "13988"},
         { 50, "13989"},
         { 60, "13990"},
         { 70, "13991"},
         { 80, "13992"},
         { 90, "13993"},
         { 100, "13994"},
         { 110, "13995"},
         { 120, "13996"},
         { 130, "13997"},
         { 140, "13998"},
         { 150, "13999"},
         { 160, "13100"},
         { 170, "13101"},
         { 180, "13102"},
         { 190, "13103"},
         { 200, "13104"},
      };

      public string GetUrl(IAvitoRequestSettings settings) {
         var urlBuilder = new StringBuilder(AVITO_MAIN_URL);
         foreach (var segment in settings.Segments) {
            urlBuilder.Append($"/{segment}");
         }

         AddParameters(urlBuilder, settings);

         try {
            var url = new Uri(urlBuilder.ToString());
            return url.ToString();
         } catch (Exception ex) {
            throw new Exception("Не получается построить урл", ex);
         }
      }

      public string GetUrl(AvitoItem item) {
         return $"{AVITO_MAIN_URL}{item.Url}";
      }

      private void AddParameters(StringBuilder urlBuilder, IAvitoRequestSettings settings) {
         const char PARAMETER_START = '?';
         if (settings.HasParameters) {
            urlBuilder.Append(PARAMETER_START);
            urlBuilder.Append(settings.DefaultParameter);
            AddDistrictsParameter(urlBuilder, settings.Districts);
            AddFactors(urlBuilder, settings);
            AddPayMaxParameter(urlBuilder, settings.PayMax);
            AddPayMinParameter(urlBuilder, settings.PayMin);
         }
      }

      private void AddPayMinParameter(StringBuilder urlBuilder, int? payMin) {
          if (payMin.HasValue) {
            urlBuilder.Append(PARAMETER_DEVIDER);
            urlBuilder.Append($"pmin={payMin.Value}");
          }
      }

      private void AddPayMaxParameter(StringBuilder urlBuilder, int? payMax) {
          if (payMax.HasValue) {
            urlBuilder.Append(PARAMETER_DEVIDER);
            urlBuilder.Append($"pmax={payMax.Value}");
          }
      }

      private void AddFactors(StringBuilder urlBuilder, IAvitoRequestSettings settings) {
         const char FACTOR_DEVIDER = '.';
         var factors = new List<string>();
         AddIfNotNull(factors, GetMaterialFactor(settings.Materials));
         AddIfNotNull(factors, GetFirstFloorFactor(settings.FirstFloor));
         AddIfNotNull(factors, GetHouseFloorFactor(settings.HouseFloor));
         AddIfNotNull(factors, GetSquareFactor(settings.MinSquare, settings.MaxSquare));

         urlBuilder.Append(PARAMETER_DEVIDER);
            var factorsParameterBuilder = new StringBuilder("f=");
            factors.ForEach(f => factorsParameterBuilder.Append($"{f}{FACTOR_DEVIDER}"));
            urlBuilder.Append(factorsParameterBuilder.ToString().TrimEnd(FACTOR_DEVIDER));
      }

      private string GetSquareFactor(int? minSquare, int? maxSquare) {
         if (minSquare.HasValue || maxSquare.HasValue) {
            var minFactorValue = !minSquare.HasValue ? 10 : minSquare.Value;
            var maxnFactorValue = !maxSquare.HasValue ? 200 : maxSquare.Value;
            return $"59_{_squareFactors[minFactorValue]}b{_squareFactors[maxnFactorValue]}";
         }

         return null;
      }

      private string GetHouseFloorFactor(int? houseFloor) {
          if (houseFloor.HasValue) {
            return $"497_{5181+houseFloor.Value}b";
          }

         return null;
      }

      private string GetFirstFloorFactor(int? firstFloor) {
         if (firstFloor.HasValue) {
            return $"496_{5119+firstFloor.Value}b";
         }

         return null;
      }

      private void AddDistrictsParameter(StringBuilder urlBuilder, District? districts) {
         const char DISTRICT_DEVIDER = '-';
         if (districts.HasValue) {
            urlBuilder.Append(PARAMETER_DEVIDER);
            var districtParameterBuilder = new StringBuilder("district=");
            foreach (var district in Enum.GetValues(typeof(District))) {
               if (((int)districts.Value & (int)district) != 0) {
                  districtParameterBuilder.Append($"{_districtParameters[(District)district]}{DISTRICT_DEVIDER}");
               }
            }
            urlBuilder.Append(districtParameterBuilder.ToString().TrimEnd(DISTRICT_DEVIDER));
         }
      }

      private string GetMaterialFactor(Material? materials) {
         const char MATERIAL_DEVIDER = '-';
         if (materials.HasValue) {
            var materialFactorBuilder = new StringBuilder("498_");
            foreach (var material in Enum.GetValues(typeof(Material))) {
               if (((int)materials.Value & (int)material) != 0) {
                  materialFactorBuilder.Append($"{_materialParameters[(Material)material]}{MATERIAL_DEVIDER}");
               }
            }

            return materialFactorBuilder.ToString().Trim(MATERIAL_DEVIDER);
         }

         return null;
      }

      private void AddIfNotNull(List<string> list, string item) {
         if (item != null) {
            list.Add(item);
         }
      }
   }
}
