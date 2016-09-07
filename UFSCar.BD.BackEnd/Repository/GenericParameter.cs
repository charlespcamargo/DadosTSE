using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Repository
{
    public class GenericParameter
    {

        internal static DbParameter Create(string nomeParametro, System.Data.DbType dbType)
        {
            string providerName = System.Configuration.ConfigurationManager.ConnectionStrings["IharaContext"].ProviderName;
            DbParameter parametro = null;

            parametro = new SqlParameter(nomeParametro.Replace("@", ""), DeParaSqlParameter(dbType));

            return parametro;
        }

        internal static DbParameter Create(string nomeParametro, System.Data.DbType dbType, object valor)
        {
            DbParameter parametro = Create(nomeParametro, dbType);
            if (valor != null)
            {
                parametro.Value = valor;
            }
            else
            {
                parametro.Value = DBNull.Value;
            }

            return parametro;
        }

        internal static DbParameter Create(string nomeParametro, DbType dbType, int size, bool isNullable, int precision, int scale)
        {
            string providerName = System.Configuration.ConfigurationManager.ConnectionStrings["IharaContext"].ProviderName;
            DbParameter parametro = null;

            parametro = new SqlParameter(nomeParametro.Replace("@", ""), DeParaSqlParameter(dbType), size);
            ((SqlParameter)parametro).Precision = (byte)precision;
            ((SqlParameter)parametro).IsNullable = isNullable;
            ((SqlParameter)parametro).Scale = (byte)scale;


            return parametro;
        }


        private static SqlDbType DeParaSqlParameter(DbType dbType)
        {
            SqlDbType param = SqlDbType.VarChar;
            switch (dbType)
            {
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                    param = SqlDbType.VarChar;
                    break;
                case DbType.Binary:
                case DbType.Byte:
                    param = SqlDbType.Binary;
                    break;
                case DbType.Boolean:
                    param = SqlDbType.Bit;
                    break;
                case DbType.Currency:
                case DbType.VarNumeric:
                    param = SqlDbType.Decimal;
                    break;
                case DbType.Date:
                    param = SqlDbType.Date;
                    break;
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                    param = SqlDbType.DateTime;
                    break;
                case DbType.Decimal:
                    param = SqlDbType.Decimal;
                    break;
                case DbType.Double:
                    param = SqlDbType.Decimal;
                    break;
                case DbType.Guid:
                    param = SqlDbType.UniqueIdentifier;
                    break;
                case DbType.Object:
                    param = SqlDbType.VarChar;
                    break;
                case DbType.Single:
                    param = SqlDbType.VarChar;
                    break;
                case DbType.Xml:
                    param = SqlDbType.Xml;
                    break;
                case DbType.SByte:
                    param = SqlDbType.Bit;
                    break;
                case DbType.Time:
                    param = SqlDbType.Time;
                    break;
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                    param = SqlDbType.Int;
                    break;
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                    param = SqlDbType.Int;
                    break;
            }

            return param;
        }
    }


}