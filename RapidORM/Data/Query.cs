using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RapidORM.Common;

namespace RapidORM.Data
{
    public class Query<T> : Field<T>
    {        
        protected string CreateInsertQueryWithoutPrimaryKey(T o)
        {
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string sqlQuery = "insert into " + GetTableName() + " (";
            string separator = "";
            foreach (PropertyInfo field in fields)
            {
                sqlQuery += separator + GetColumnName(field);
                separator = ",";
            }

            sqlQuery += ") values (";
            separator = "";
            foreach (PropertyInfo field in fields)
            {
                string strValue = field.GetValue(o, null).ToString();
                sqlQuery += separator + FormatRawSqlQuery(strValue, field);
                separator = ",";
            }

            sqlQuery += ");";

            return sqlQuery;
        }

        protected string CreateInsertQuery(T o)
        {
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string sqlQuery = "insert into " + GetTableName() + " (";
            string separator = "";
            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field))
                {
                    continue; 
                }

                sqlQuery += separator + GetColumnName(field);
                separator = ",";
            }

            sqlQuery += ") values (";
            separator = "";
            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field))
                {
                    continue;
                }

                string strValue = field.GetValue(o, null).ToString();
                sqlQuery += separator + FormatRawSqlQuery(strValue, field);
                separator = ",";
            }

            sqlQuery += ");";

            return sqlQuery;
        }

        protected ImageParameterQueryContainer CreateInsertQueryWithImage(T o)
        {
            var imageParameterList = new List<ImageParameter>();
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string sqlQuery = "insert into " + GetTableName() + " (";
            string separator = "";
            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field))
                {
                    continue; 
                }

                sqlQuery += separator + GetColumnName(field);
                separator = ",";
            }

            sqlQuery += ") values (";
            separator = "";
            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field))
                {
                    continue; 
                }

                string fieldValue = string.Empty;
                if (IsConfirmedImage(field))
                {
                    byte[] byteArrayValue = (byte[])field.GetValue(o, null);
                    imageParameterList.Add(new ImageParameter
                    {
                        Parameter = string.Format("@{0}", GetColumnName(field)),
                        Value = byteArrayValue
                    });
                }
                else
                {
                    fieldValue = field.GetValue(o, null).ToString();
                }

                sqlQuery += separator + FormatRawSqlQuery(fieldValue, field);
                separator = ",";
            }

            sqlQuery += ");";

            return new ImageParameterQueryContainer
            {
                ImageParameterList = imageParameterList,
                SqlQuery = sqlQuery
            };
        }
        
        protected string CreateUpdateQuery(T o)
        {
            string uniqueField = GetPrimaryKey().Name;

            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string sqlQuery = "update " + GetTableName() + " set ";
            string separator = string.Empty;

            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field)) continue; 

                string strValue = field.GetValue(o, null).ToString();
                sqlQuery += separator + GetColumnName(field);
                sqlQuery += " = " + FormatRawSqlQuery(strValue, field);
                separator = ",";
            }

            PropertyInfo piUnique = typeof(T).GetProperty(uniqueField);
            string strUnique = piUnique.GetValue(o, null).ToString();
            strUnique = strUnique.Replace("'", "\"");
            sqlQuery += " where " + uniqueField + "= '" + strUnique + "'";

            return sqlQuery;
        }

        protected ImageParameterQueryContainer CreateUpdateQueryWithImage(T o)
        {
            var imageParameterList = new List<ImageParameter>();
            string uniqueField = GetPrimaryKey().Name;

            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string sqlQuery = "update " + GetTableName() + " set ";
            string separator = string.Empty;

            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field)) continue; 

                string fieldValue = string.Empty;
                if (IsConfirmedImage(field))
                {
                    byte[] byteArrayValue = (byte[])field.GetValue(o, null);
                    imageParameterList.Add(new ImageParameter
                    {
                        Parameter = string.Format("@{0}", GetColumnName(field)),
                        Value = byteArrayValue
                    });
                }
                else
                {
                    fieldValue = field.GetValue(o, null).ToString();
                }

                sqlQuery += separator + GetColumnName(field);
                sqlQuery += " = " + FormatRawSqlQuery(fieldValue, field);
                separator = ",";
            }

            PropertyInfo piUnique = typeof(T).GetProperty(uniqueField);
            string strUnique = piUnique.GetValue(o, null).ToString();
            strUnique = strUnique.Replace("'", "\"");
            sqlQuery += " where " + uniqueField + "= '" + strUnique + "'";

            return new ImageParameterQueryContainer
            {
                ImageParameterList = imageParameterList,
                SqlQuery = sqlQuery
            };
        }

        protected string FormatRawSqlQuery(string value, PropertyInfo field)
        {
            string rawQuery = string.Empty;
            if (IsConfirmedImage(field))
            {
                rawQuery = string.Format("@{0}", GetColumnName(field));
            }
            else
            {
                if (IsStringType(field))
                {
                    rawQuery = "N'" + value.Replace("'", "\"") + "'";
                }
                else if (IsDecimalType(field))
                {
                    rawQuery = value.Replace(",", ".");
                }
                else
                {
                    rawQuery = value;
                }
            }

            return rawQuery;
        }
    }
}
