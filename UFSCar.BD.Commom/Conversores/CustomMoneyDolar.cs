using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Conversores
{
    public class CustomMoneyDolar : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && !String.IsNullOrEmpty(reader.Value.ToString()))
                return Convert.ToDecimal(reader.Value, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            else
                return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string valor = "";
            if (value != null)
            {
                valor = Convert.ToDecimal(value).ToString("N2", new CultureInfo("en-US")).ToString();
            }

            writer.WriteValue(valor);

        }
    }
}
