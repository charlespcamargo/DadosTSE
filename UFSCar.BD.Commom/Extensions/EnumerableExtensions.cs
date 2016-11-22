using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            var grouped = source.GroupBy(selector);
            var moreThen1 = grouped.Where(i => i.IsMultiple());
            return moreThen1.SelectMany(i => i);
        }

        public static IEnumerable<TSource> Duplicates<TSource, TKey>(this IEnumerable<TSource> source)
        {
            return source.Duplicates(i => i);
        }

        public static bool IsMultiple<T>(this IEnumerable<T> source)
        {
            var enumerator = source.GetEnumerator();
            return enumerator.MoveNext() && enumerator.MoveNext();
        }

        public static String GetDescription(this Object object_)
        {
            return GetAttributeProperty<String>(object_, "Display", typeof(DisplayNameAttribute), "Display");
        }

        public static String GetValueByName(this Object object_, string Name)
        {
            return GetAttributeProperty<String>(object_, Name, typeof(DisplayNameAttribute), Name);
        }

        /// <summary>
        /// Get a string property of an attribute of this object. The text property must have the same name of the attribute.
        /// </summary>
        /// <returns>The attribute property.</returns>
        public static T GetAttributeProperty<T>(this Object object_, String attributeName, Type attributeType, String propertyName)
        {
            Type objectType = object_.GetType();
            Object[] attributes = null;

            if (object_.GetType().BaseType == typeof(Enum))
                attributes = objectType.GetField(object_.ToString()).GetCustomAttributes(attributeType, false);
            else
                attributes = objectType.GetCustomAttributes(attributeType, false);

            if (attributes.Length > 0)
                return (T)attributeType.GetProperty(attributeName).GetValue(attributes[0], null);
            return default(T);
        }

    }
}
