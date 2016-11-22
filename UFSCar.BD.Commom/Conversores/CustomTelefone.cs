using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BackEnd.Common.Conversores
{
    public class CustomTelefone : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && !String.IsNullOrEmpty(reader.Value.ToString()))
                return reader.Value;
            else
                return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string valor = "";

            if (value != null)
            {
                valor = value.ToString().Trim();

                valor = valor.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", string.Empty);

                #region Algumas possibilidades de fomatacao

                // [13] => (15)999999999  => (15)99999-9999
                if (valor.Length == 13)
                    valor = valor.Insert(9, "-");

                // [12] => (15)33333333  => (15)3333-3333
                else if (valor.Length == 12)
                    valor = valor.Insert(8, "-");

                // [11] => 15999999999 => (15)99999-9999
                else if (valor.Length == 11)
                    valor = valor.Insert(7, "-").Insert(2, ")").Insert(0, "(");

                // [10] => 1533333333 => (15)3333-3333
                else if (valor.Length == 10)
                    valor = valor.Insert(6, "-").Insert(2, ")").Insert(0, "(");

                // [08] => 33333333   => 3333-3333
                else if (valor.Length == 8)
                    valor = valor.Insert(4, "-");

                #endregion Algumas possibilidades de fomatacao
            }

            writer.WriteValue(valor);

        }
    }
}
