using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RapidORM.Data;
using RapidORM.Data.Common;
using RapidORM.Helpers;

namespace RapidORM.Data.SQLite
{
    public class SQLiteEntity<T> : SQLiteTransaction<T>, IDBEntity<T>
    {
        public string tableName = string.Empty;

        public SQLiteEntity()
        {
            tableName = GetTableName();
        }

        #region Save Changes
        public void SaveChanges(T o)
        {
            if (CheckIfAlreadyExists(o))
            {
                UpdateObject(o);
            }
            else
            {
                InsertObject(o);
            }
        }

        public void SaveChangesWithImage(T o)
        {
            if (CheckIfAlreadyExists(o))
            {
                UpdateObjectWithImage(o);
            }
            else
            {
                InsertObjectWithImage(o);
            }
        }
        #endregion

        #region Update
        public void UpdateObject(T o)
        {
            try
            {
                ExecuteNonQuery(CreateUpdateQuery(o));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void UpdateObjectWithImage(T o)
        {
            try
            {
                ExecuteNonQuery(CreateUpdateQueryWithImage(o));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region Create
        public int InsertObjectAndReturnsId(T o)
        {
            try
            {
                return Convert.ToInt32(ExecuteScalar(CreateInsertQuery(o, SpecialCharacter.No)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void InsertObject(T o)
        {
            try
            {
                ExecuteNonQuery(CreateInsertQuery(o, SpecialCharacter.No));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void InsertObjectWithoutPrimaryKey(T o)
        {
            try
            {
                ExecuteNonQuery(CreateInsertQueryWithoutPrimaryKey(o));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void InsertObjectWithImage(T o)
        {
            try
            {
                ExecuteNonQuery(CreateInsertQueryWithImage(o));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region Delete
        public void DeleteObject(T o)
        {
            PropertyInfo primaryKey = GetPrimaryKey();
            DeleteObject(o, primaryKey);
        }

        public void DeleteObject(T o, string field)
        {
            DeleteObject(o, GetField(field));
        }
        #endregion

        #region Retrieval
        public IEnumerable<T> GetAllObjects()
        {
            IEnumerable<T> objects = null;
            string table = tableName;

            string strSQL = "select * from " + table;

            objects = GetValues(strSQL);

            return objects;
        }

        public IEnumerable<T> GetObjectsByCriteria(string field, string criteria)
        {
            return GetObjectsByCriteria(GetField(field), criteria);
        }

        public IEnumerable<T> GetObjectsByCriteria(List<SearchCriteria> searchCriteriaList)
        {
            return GetObjectsByMultipleCriterias(searchCriteriaList);
        }

        public IEnumerable<T> GetObjectsByCriteria(SearchCriteria searchCriteria)
        {
            return GetObjectsByCriteria(GetField(searchCriteria.Column), searchCriteria.Value);
        }

        public IEnumerable<T> GetObjectsByCriteria(string criteria)
        {
            PropertyInfo primaryKey = GetPrimaryKey();
            return GetObjectsByCriteria(primaryKey, criteria);
        }

        public IEnumerable<T> GetObjectsByMultipleCriterias(List<SearchCriteria> searchCriteriaList)
        {
            string table = tableName;

            string strSQL = "select * from " + table + " where ";
            PropertyInfo field = null;
            foreach (var searchCriteria in searchCriteriaList)
            {
                field = GetField(searchCriteria.Column);
                strSQL += GetColumnName(field) + "=" + FormatRawSqlQuery(searchCriteria.Value, field, SpecialCharacter.No) + " and ";
            }

            strSQL = strSQL.Remove(strSQL.Length - 5);

            return GetValues(strSQL);
        }
        #endregion 
    }
}
