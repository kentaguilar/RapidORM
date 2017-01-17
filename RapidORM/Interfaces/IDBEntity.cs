using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Common;

namespace RapidORM.Interfaces
{
    public interface IDBEntity<T>
    {
        /// <summary>
        /// All-in-one saving of changes. 
        /// It automatically detects if object provided will be used for insert or update
        /// </summary>
        /// <param name="o"></param>
        void SaveChanges(T o);

        /// <summary>
        /// All-in-one saving of changes with blob images
        /// It automatically detects if object provided will be used for insert or update
        /// </summary>
        /// <param name="o"></param>
        void SaveChangesWithImage(T o);

        /// <summary>
        /// Updates an object. Also used by Save Changes method
        /// </summary>
        /// <param name="o"></param>
        void UpdateObject(T o);

        /// <summary>
        /// Updates an object with an image. Also used by Save Changes method
        /// </summary>
        /// <param name="o"></param>
        void UpdateObjectWithImage(T o);

        /// <summary>
        /// Inserts an object to database and returns the record id
        /// </summary>
        /// <param name="o"></param>
        int InsertObjectAndReturnsId(T o);

        /// <summary>
        /// Straightforward object insertion
        /// </summary>
        /// <param name="o"></param>
        void InsertObject(T o);

        /// <summary>
        /// Save an object with image(byte array)
        /// </summary>
        /// <param name="o"></param>
        void InsertObjectWithImage(T o);

        /// <summary>
        /// Save an object without primary key
        /// </summary>
        /// <param name="o"></param>
        void InsertObjectWithoutPrimaryKey(T o);

        /// <summary>
        /// Delete a record with an object
        /// </summary>
        /// <param name="o"></param>
        void DeleteObject(T o);

        /// <summary>
        /// Delete a record using a class property name
        /// </summary>
        /// <param name="o"></param>
        /// <param name="field"></param>
        void DeleteObject(T o, string field);

        /// <summary>
        /// Get all table records
        /// </summary>        
        IEnumerable<T> GetAllObjects();

        /// <summary>
        /// Retrieve record(s) using a class property name
        /// </summary>
        /// <param name="field"></param>
        /// <param name="criteria"></param>
        IEnumerable<T> GetObjectsByCriteria(string field, string criteria);

        /// <summary>
        /// Retrieve record(s) using multiple conditions. Alternative to GetObjectsByMultipleCriterias
        /// </summary>
        /// <param name="searchCriteriaList"></param>
        IEnumerable<T> GetObjectsByCriteria(List<SearchCriteria> searchCriteriaList);

        /// <summary>
        /// Retrieve record(s) with a single condition only
        /// </summary>
        /// <param name="searchCriteria"></param>
        IEnumerable<T> GetObjectsByCriteria(SearchCriteria searchCriteria);

        /// <summary>
        /// Retrieve a condition using a class property name(string)
        /// </summary>
        /// <param name="criteria"></param>
        IEnumerable<T> GetObjectsByCriteria(string criteria);

        /// <summary>
        /// Retrieve record(s) using multiple conditions
        /// </summary>
        /// <param name="searchCriteriaList"></param>
        IEnumerable<T> GetObjectsByMultipleCriterias(List<SearchCriteria> searchCriteriaList);
    }
}
