using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Conversores
{
    public class CustomDate : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && !String.IsNullOrEmpty(reader.Value.ToString()))
                return Convert.ToDateTime(reader.Value, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            else
                return null;

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string valor = "";
            if (value != null)
            {
                valor = Convert.ToDateTime(value).ToString("dd/MM/yyyy", new CultureInfo("pt-BR")).ToString();
            }

            writer.WriteValue(valor);

        }
    }

    public class CustomDateTime : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && !String.IsNullOrEmpty(reader.Value.ToString()))
                return Convert.ToDateTime(reader.Value, System.Globalization.CultureInfo.GetCultureInfo("en-US"));
            else
                return null;

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string valor = "";
            if (value != null)
            {
                valor = Convert.ToDateTime(value).ToString("dd/MM/yyyy HH:mm", new CultureInfo("pt-BR")).ToString();
            }

            writer.WriteValue(valor);

        }
    }
}
