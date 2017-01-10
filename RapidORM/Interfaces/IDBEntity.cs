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
        /// All-in-one saving of changes
        /// </summary>
        /// <param name="o"></param>
        void SaveChanges(T o);

        /// <summary>
        /// All-in-one saving of changes with blob images
        /// </summary>
        /// <param name="o"></param>
        void SaveChangesWithImage(T o);

        void UpdateObject(T o);
        void UpdateObjectWithImage(T o);

        int InsertObjectAndReturnsId(T o);
        void InsertObject(T o);
        void InsertObjectWithImage(T o);
        void InsertObjectWithoutPrimaryKey(T o);

        void DeleteObject(T o);
        void DeleteObject(T o, string field);

        List<T> GetAllObjects();
        List<T> GetObjectsByCriteria(string field, string criteria);
        List<T> GetObjectsByCriteria(List<SearchCriteria> searchCriteriaList);
        List<T> GetObjectsByCriteria(SearchCriteria searchCriteria);
        List<T> GetObjectsByCriteria(string criteria);
        List<T> GetObjectsByMultipleCriterias(List<SearchCriteria> searchCriteriaList);
    }
}
