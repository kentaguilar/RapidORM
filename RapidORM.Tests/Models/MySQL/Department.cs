﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Data.Common;
using RapidORM.Data.MySQL;

namespace RapidORM.Tests.Models.MySQL
{
    [TableName("department")]
    public class Department
    {
        [IsPrimaryKey(true)]
        [ColumnName("id")]
        public int Id { get; set; }

        [ColumnName("name")]
        public string Name { get; set; }

        [ColumnName("date_created")]
        public string DateCreated { get; set; }
        
        private IDBEntity<Department> dbEntity = null;

        public Department()
        {
            dbEntity = new MySqlEntity<Department>();
        }
        
        public Department(Dictionary<string, object> args)
        {
            Id = Convert.ToInt32(args["id"].ToString());
            Name = args["name"].ToString();
            DateCreated = args["date_created"].ToString();
        }

        #region Class Methods
        public void Save(Department department)
        {
            dbEntity.SaveChanges(department);
        }

        public int InsertDepartmentAndReturnAnId(Department department)
        {
            int returnedId = dbEntity.InsertObjectAndReturnsId(department);

            return returnedId;
        }

        public void DeleteDepartmentByPropertyName(string fieldValue)
        {
            dbEntity.DeleteObject(new Department 
            { 
                Name = fieldValue
            }, "Name");
        }

        public void DeleteDepartmentByObject(Department department)
        {
            dbEntity.DeleteObject(department);
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            IEnumerable<Department> departments = dbEntity.GetAllObjects();

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByDate(DateTime givenDate)
        {
            var departments = dbEntity.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.DateCreated),
                Value = givenDate.ToString("yyyy-MM-dd")
            });

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByStringCriteria(int id)
        {
            var departments = dbEntity.GetObjectsByCriteria("Id", id.ToString());

            return (departments.Count() > 0) ? departments : null;
        }

        public IEnumerable<Department> GetDepartmentsByMultipleCriteria(string name, DateTime givenDate)
        {
            var departments = dbEntity.GetObjectsByMultipleCriterias(new List<SearchCriteria> 
            { 
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.Name),
                    Value = name
                },
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.DateCreated),
                    Value = givenDate.ToString("yyyy-MM-dd")
                }
            });

            return (departments.Count() > 0) ? departments : null;
        }
        #endregion
    }
}
