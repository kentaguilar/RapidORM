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
        void InsertObject(T o);
        void InsertObjectWithImage(T o);
        void InsertObjectWithoutPrimaryKey(T o);

        void DeleteObject(T o);
        void DeleteObject(T o, string field);

        IEnumerable<T> GetAllObjects();
        List<T> GetObjectsByCriteria(string field, string criteria);
        List<T> GetObjectsByCriteria(List<SearchCriteria> searchCriteriaList);
        List<T> GetObjectsByCriteria(SearchCriteria searchCriteria);
        List<T> GetObjectsByCriteria(string criteria);
        List<T> GetObjectsByMultipleCriterias(List<SearchCriteria> searchCriteriaList);
    }
}
