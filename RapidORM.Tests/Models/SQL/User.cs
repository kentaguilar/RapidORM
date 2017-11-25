using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Data.Common;
using RapidORM.Data.SQL;
using System.Data;
using System.Data.Common;

namespace RapidORM.Tests.Models.SQL
{
    [TableName("SystemUser")]
    public class SystemUser
    {
        [IsPrimaryKey(true)]
        public int Id { get; set; }

        [ColumnName("Name")]
        public string Name { get; set; }

        [ColumnName("Email")]
        public string Email { get; set; }

        [ColumnName("Password")]
        public string Password { get; set; }

        [ColumnName("DesignatedPosition")]
        public string DesignatedPosition { get; set; }

        private SqlEntity<SystemUser> entity = new SqlEntity<SystemUser>();

        #region Instance Methods
        public void Save(SystemUser user)
        {
            entity.SaveChanges(user);            
        }

        public int InsertUserAndReturnAnId(SystemUser user)
        {
            int returnedId = entity.InsertObjectAndReturnsId(user);

            return returnedId;
        }

        public void DeleteUserByPropertyName(string fieldValue)
        {
            entity.DeleteObject(new SystemUser
            {
                Email = fieldValue
            }, "Email");
        }

        public void DeleteUserByObject(SystemUser user)
        {
            entity.DeleteObject(user);
        }

        public IEnumerable<SystemUser> GetAllSystemUsers()
        {
            IEnumerable<SystemUser> systemUsers = entity.GetAllObjects();

            return (systemUsers.Count() > 0) ? systemUsers : null;
        }

        public IEnumerable<SystemUser> GetUserByDesignatedPosition(string designatedPosition)
        {
            var systemUsers = entity.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.DesignatedPosition),
                Value = designatedPosition
            });

            return (systemUsers.Count() > 0) ? systemUsers : null;
        }

        public IEnumerable<SystemUser> GetUsersByStringCriteria(int id)
        {
            var systemUsers = entity.GetObjectsByCriteria("Id", id.ToString());

            return (systemUsers.Count() > 0) ? systemUsers : null;
        }

        public IEnumerable<SystemUser> GetUsersByMultipleCriteria(string name, string designatedPosition)
        {
            var systemUsers = entity.GetObjectsByMultipleCriterias(new List<SearchCriteria> 
            { 
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.Name),
                    Value = name
                },
                new SearchCriteria
                {
                    Column = PropertyHelper.GetPropertyName(() => this.DesignatedPosition),
                    Value = designatedPosition
                }
            });

            return (systemUsers.Count() > 0) ? systemUsers : null;
        }
        #endregion
    }
}
