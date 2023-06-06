using Newtonsoft.Json;

namespace Utils
{
    public static class StringExtension
    {
        public static string ToMoneyFormat(this int chip) => $"{chip:N0}";

        public static string PriceToSymbol(this string value) =>
            value.Replace("USD ", "$").Replace("EUR ", "€").Replace("GPB ", "£").Replace("TRY ", "₺");
        public static string JsonSerialize(this object value) => JsonConvert.SerializeObject(value);
        public static T JsonDeserialize<T>(this string value) => JsonConvert.DeserializeObject<T>(value);
        public static string ConvertToDecimal(int value) => (value/1000).ToString("D")+"K";



    }
}