using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Util
{
    public static class Comparar
    {
        public static bool PropriedadesIguais<T>(T self, T to, string ignore) where T : class
        {
            List<bool> alterados = new List<bool>();
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore.Split(',').ToList());
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            alterados.Add(false);
                        }
                    }
                }
                return alterados.Count > 0;
            }
            return self == to;
        }
    }
}
