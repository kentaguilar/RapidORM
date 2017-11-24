using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using RapidORM.Data.Common;

namespace RapidORM.Data
{
    public class Field<T>
    {
        protected bool IsPrimaryKeyWithIdentity(PropertyInfo field)
        {
            bool isPrimaryKeyWithIdentity = false;
            Attribute customAttribute = field.GetCustomAttribute(typeof(IsPrimaryKey));

            if (customAttribute != null && customAttribute.GetType() == typeof(IsPrimaryKey) &&
                ((IsPrimaryKey)customAttribute).IsIdentity)
            {
                isPrimaryKeyWithIdentity = true;
            }

            return isPrimaryKeyWithIdentity;
        }

        protected bool IsConfirmedImage(PropertyInfo field)
        {
            bool isConfirmedImage = false;
            Attribute customAttribute = field.GetCustomAttribute(typeof(IsImage));

            if (customAttribute != null && customAttribute.GetType() == typeof(IsImage) &&
                ((IsImage)customAttribute).Value)
            {
                isConfirmedImage = true;
            }

            return isConfirmedImage;
        }

        protected string GetTableName()
        {
            var tableAttribute = typeof(T).GetCustomAttribute(typeof(TableName));
            string tableName = string.Empty;
            if (tableAttribute != null)
            {
                tableName = ((TableName)tableAttribute).Name;
            }
            return tableName;
        }

        protected List<string> GetFieldsColumnNames()
        {
            var propertyList = new List<string>();
            var newPropertyInfo = GetFields();
            foreach (PropertyInfo field in newPropertyInfo)
            {
                var customAttribute = field.GetCustomAttribute(typeof(ColumnName));
                if (customAttribute != null)
                {
                    propertyList.Add(((ColumnName)customAttribute).Name);
                }
                else
                {
                    propertyList.Add(field.Name);
                }
            }

            return propertyList;
        }

        protected PropertyInfo GetField(string name)
        {            
            PropertyInfo field = typeof(T).GetProperty(name);

            return (PropertyInfo)field;
        }

        protected string GetColumnName(PropertyInfo field)
        {
            string realColumnName = string.Empty;
            var customAttribute = field.GetCustomAttribute(typeof(ColumnName));

            return (customAttribute != null) ? ((ColumnName)customAttribute).Name : field.Name;
        }

        protected PropertyInfo GetPrimaryKey()
        {
            PropertyInfo primaryKey = null;

            foreach (PropertyInfo field in GetFields())
            {
                Attribute customAttribute = field.GetCustomAttribute(typeof(IsPrimaryKey));
                if (customAttribute != null)
                {
                    primaryKey = field;
                    break;
                }
            }

            return primaryKey;
        }

        protected PropertyInfo[] GetFields()
        {
            PropertyInfo[] fields = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return fields;
        }

        #region Conditional Methods
        protected bool IsStringType(PropertyInfo field)
        {
            bool isStringType = false;
            if (field.PropertyType == typeof(System.String))
            {
                isStringType = true;
            }
            return isStringType;
        }

        protected bool IsValidPath(string value)
        {
            bool isByteArrayType = false;
            try
            {
                var givenPath = Path.GetFullPath(value);
                isByteArrayType = true;
            }
            catch (Exception ex)
            {
                isByteArrayType = false;
            }

            return isByteArrayType;
        }

        protected bool IsDecimalType(PropertyInfo field)
        {
            bool isDecimalType = false;
            if (field.PropertyType == typeof(System.Decimal))
            {
                isDecimalType = true;
            }
            return isDecimalType;
        }

        #endregion
    }
}
