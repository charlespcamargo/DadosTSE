using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

namespace UFSCar.BD.Commom.Reflector
{
    /// <summary>
    /// Usa Reflection para retornar dados uteis, em geral sobre os metadados das entidades
    /// </summary>
    public class RefletorDinamico
    {
        // Tipo da classe sendo manipulada
        Type T;

        public RefletorDinamico(Type T)
        {
            this.T = T;
        }

        #region refletor Propriedades

        /// <summary>
        /// Retorna as informações relacionadas às propriedades do tipo
        /// </summary>
        public PropertyInfo[] Propriedades
        {
            get
            {
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
                return (PropertyInfo[])T.GetProperties(flags);
            }
        }

        public PropertyInfo Propriedade(string name, bool objRico)
        {
            foreach (PropertyInfo pi in Propriedades)
            {
                if (pi.Name.ToLower() == name.ToLower())
                {
                    return pi;
                }
                else
                {
                    if (objRico)
                    {
                        string[] nomeRico = name.Split('.');
                        if (pi.Name.ToLower() == nomeRico[0].ToLower())
                        {
                            Type tipoRico = pi.PropertyType;
                            return PopularObjetoRico(tipoRico, nomeRico[1]);
                        }
                    }
                    else
                    {
                        object[] atributosProps = pi.GetCustomAttributes(false);
                        List<object> lstProps = atributosProps.Where(p => p.GetType() == typeof(ColumnAttribute)).ToList();
                        if (lstProps.Count > 0)
                        {
                            object atributo = lstProps.First();
                            if (((ColumnAttribute)atributo).Name.ToLower() == name.ToLower())
                            {
                                return pi;
                            }
                        }
                    }
                }
            }
            return null;
        }
        
        #endregion

        #region refletor Atributos

        /// <summary>
        /// Retorna os atributos (marcações) de um membro (em geral PropertyInfo)
        /// </summary>
        /// <typeparam name="E">Tipo do Attribute procurado (ex. EntidadeAttribute, CampoAttribute, Serializable, ...)</typeparam>
        /// <param name="pi">Informação sobre o membro (em geral PropertyInfo)</param>
        /// <returns>vetor de Attributes</returns>
        public E[] Atributos<E>(MemberInfo pi)
        {
            return pi.GetCustomAttributes(typeof(E), false) as E[];
        }

        /// <summary>
        /// Retorna o primeiro atributo (marcação) de um membro (em geral PropertyInfo)
        /// </summary>
        /// <typeparam name="E">Tipo do Attribute procurado (ex. EntidadeAttribute, CampoAttribute, Serializable, ...)</typeparam>
        /// <param name="pi">Informação sobre o membro (em geral PropertyInfo)</param>
        /// <returns>vetor de Attributes</returns>
        public E Atributo<E>(MemberInfo pi)
        {
            E[] attributes = Atributos<E>(pi);
            if (attributes.Length > 0)
                return attributes[0];
            return default(E);
        }

        #endregion

        #region Métodos Auxiliares

        public DbType RetornaDbType(string campo)
        {
            foreach (PropertyInfo pi in Propriedades)
            {
                if (pi.Name == campo)
                {
                    if (pi.PropertyType == typeof(DateTime)) return DbType.DateTime;
                    if (pi.PropertyType == typeof(string)) return DbType.String;
                    if (pi.PropertyType == typeof(int)) return DbType.Int32;
                    if (pi.PropertyType == typeof(long)) return DbType.Int64;
                    if (pi.PropertyType == typeof(float)) return DbType.Decimal;
                    if (pi.PropertyType == typeof(double)) return DbType.Double;
                    if (pi.PropertyType == typeof(decimal)) return DbType.Decimal;
                    if (pi.PropertyType == typeof(char)) return DbType.String;
                    if (pi.PropertyType == typeof(bool))
                        return DbType.Boolean;
                }
            }

            return DbType.String;
        }

        public object GetEnumDescription(System.Type value, string nomeCampo)
        {
            FieldInfo[] fis = value.GetFields();
            foreach (FieldInfo fi in fis)
            {
                if (fi.Name == nomeCampo)
                {
                    return Atributo<DescriptionAttribute>(fi).Description;
                }
            }
            return null;
        }

        public object GetConvertedObject(Type type, object o)
        {
            return GetConvertedObject(type, o, new CultureInfo("pt-BR"));
        }

        public object GetConvertedObject(Type type, object o,CultureInfo Cinfo)
        {
            // Nullable Enum
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>))
            {
                Type[] typeCol = type.GetGenericArguments();
                Type nullableType;
                if (typeCol.Length > 0)
                {
                    nullableType = typeCol[0];
                    if (nullableType.BaseType == typeof(Enum))
                    {
                        return Enum.Parse(nullableType, o.ToString(), true);
                    }
                }
            }
            if (type.IsEnum) return Convert.ToInt32(Enum.Parse(type, o.ToString(), true));
            if (type == typeof(byte[])) return o.Equals(DBNull.Value) ? null : o;
            if (type == typeof(DateTime)) return Convert.ToDateTime(o, Cinfo);
            if (type == typeof(TimeSpan)) return (TimeSpan)o;
            if (type == typeof(string)) return Convert.ToString(o);
            if (type == typeof(int)) return Convert.ToInt32(o);
            if (type == typeof(uint)) return Convert.ToInt32(o);
            if (type == typeof(long)) return Convert.ToInt64(o);
            if (type == typeof(float)) return float.Parse(o.ToString());
            if (type == typeof(double)) return Convert.ToDouble(o);
            if (type == typeof(decimal)) return string.IsNullOrEmpty(o.ToString()) ? 0 : Convert.ToDecimal(o, Cinfo);
            if (type == typeof(char)) return Convert.ToChar(o);
            if (type == typeof(byte)) return Convert.ToByte(o);
            if (type == typeof(bool)) return Convert.ToBoolean(o);

            if (type == typeof(long?)) return Convert.ToInt64(o);
            if (type == typeof(DateTime?)) return (o == null ? default(DateTime?) : Convert.ToDateTime(o,Cinfo));
            if (type == typeof(bool?)) return (o == null ? default(bool?) : Convert.ToBoolean(o));
            if (type == typeof(decimal?)) return (o == null ? default(decimal?) : Convert.ToDecimal(o,Cinfo));
            if (type == typeof(int?)) return (o == null ? default(int?) : (int)(o));

            return o;
        }

        public object GetDefaultType(Type type)
        {
            if (type == typeof(DateTime)) return default(DateTime);
            if (type == typeof(TimeSpan)) return default(TimeSpan);
            if (type == typeof(string)) return string.Empty;
            if (type == typeof(int)) return default(int);
            if (type == typeof(uint)) return default(uint);
            if (type == typeof(long)) return default(long);
            if (type == typeof(float)) return default(float);
            if (type == typeof(double)) return default(double);
            if (type == typeof(decimal)) return default(decimal);
            if (type == typeof(char)) return default(char);
            if (type == typeof(byte)) return default(byte);
            if (type == typeof(bool)) return default(bool);
            return null;
        }


        private static PropertyInfo PopularObjetoRico(Type tipoRico, string nomeCampo)
        {
            RefletorDinamico refletor = new RefletorDinamico(tipoRico);
            object novo = Activator.CreateInstance(tipoRico);
            PropertyInfo pii = refletor.Propriedade(nomeCampo, false);
          
            return pii;
        }

        #endregion


        public Object GetPropValueObjRico(String name, Object obj)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

      
    }

}
