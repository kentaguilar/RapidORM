﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RapidORM.Data.Common;
using RapidORM.Helpers;

namespace RapidORM.Data
{
    public class Query<T> : Field<T>
    {        
        protected string CreateInsertQueryWithoutPrimaryKey(T o, SpecialCharacter specialCharacter = SpecialCharacter.Yes)
        {
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);            

            string query = "insert into " + GetTableName() + " (";
            string separator = "";
            foreach (PropertyInfo field in fields)
            {
                query += separator + GetColumnName(field);
                separator = ",";
            }

            query += ") values (";
            separator = "";
            foreach (PropertyInfo field in fields)
            {
                string strValue = field.GetValue(o, null).ToString();
                query += separator + FormatRawSqlQuery(strValue, field, specialCharacter);
                separator = ",";
            }

            query += ");";

            return query;
        }

        protected string CreateInsertQuery(T o, SpecialCharacter specialCharacter = SpecialCharacter.Yes)
        {
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string query = "insert into " + GetTableName() + " (";
            string separator = "";
            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field))
                {
                    continue; 
                }

                query += separator + GetColumnName(field);
                separator = ",";
            }

            query += ") values (";
            separator = "";
            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field))
                {
                    continue;
                }

                string strValue = field.GetValue(o, null).ToString();
                query += separator + FormatRawSqlQuery(strValue, field, specialCharacter);
                separator = ",";
            }

            query += ");";

            Console.WriteLine(query);

            return query;
        }

        protected ImageParameterQueryContainer CreateInsertQueryWithImage(T o, SpecialCharacter specialCharacter = SpecialCharacter.Yes)
        {
            var imageParameterList = new List<ImageParameter>();
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            string query = "insert into " + GetTableName() + " (";
            string separator = "";
            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field))
                {
                    continue; 
                }

                query += separator + GetColumnName(field);
                separator = ",";
            }

            query += ") values (";
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

                query += separator + FormatRawSqlQuery(fieldValue, field, specialCharacter);
                separator = ",";
            }

            query += ");";

            return new ImageParameterQueryContainer
            {
                ImageParameterList = imageParameterList,
                SqlQuery = query
            };
        }

        protected string CreateUpdateQuery(T o, SpecialCharacter specialCharacter = SpecialCharacter.Yes)
        {
            string uniqueField = GetPrimaryKey().Name;

            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string query = "update " + GetTableName() + " set ";
            string separator = string.Empty;

            foreach (PropertyInfo field in fields)
            {
                if (IsPrimaryKeyWithIdentity(field)) continue; 

                string strValue = field.GetValue(o, null).ToString();
                query += separator + GetColumnName(field);
                query += " = " + FormatRawSqlQuery(strValue, field, specialCharacter);
                separator = ",";
            }

            PropertyInfo piUnique = typeof(T).GetProperty(uniqueField);
            string strUnique = piUnique.GetValue(o, null).ToString();
            strUnique = strUnique.Replace("'", "\"");
            query += " where " + uniqueField + "= '" + strUnique + "'";

            return query;
        }

        protected ImageParameterQueryContainer CreateUpdateQueryWithImage(T o, SpecialCharacter specialCharacter = SpecialCharacter.Yes)
        {
            var imageParameterList = new List<ImageParameter>();
            string uniqueField = GetPrimaryKey().Name;

            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string query = "update " + GetTableName() + " set ";
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

                query += separator + GetColumnName(field);
                query += " = " + FormatRawSqlQuery(fieldValue, field, specialCharacter);
                separator = ",";
            }

            PropertyInfo piUnique = typeof(T).GetProperty(uniqueField);
            string strUnique = piUnique.GetValue(o, null).ToString();
            strUnique = strUnique.Replace("'", "\"");
            query += " where " + uniqueField + "= '" + strUnique + "'";

            return new ImageParameterQueryContainer
            {
                ImageParameterList = imageParameterList,
                SqlQuery = query
            };
        }

        protected string FormatRawSqlQuery(string value, PropertyInfo field, SpecialCharacter specialCharacter = SpecialCharacter.Yes)
        {            
            string query = string.Empty;
            if (IsConfirmedImage(field))
            {
                query = string.Format("@{0}", GetColumnName(field));
            }
            else
            {
                if (IsStringType(field))
                {
                    query = (specialCharacter == SpecialCharacter.Yes ? "N" : string.Empty) + ("'" + value.Replace("'", "\"") + "'");                    
                }
                else if (IsTimeSpanType(field))
                {
                    query = ("'" + value.Replace("'", "\"") + "'");
                }
                else if (IsDecimalType(field))
                {
                    query = value.Replace(",", ".");
                }
                else if (IsDateTimeType(field))
                {
                    query = string.Format("'{0}'", Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    query = value;
                }
            }
            
            return query;
        }
    }
}
