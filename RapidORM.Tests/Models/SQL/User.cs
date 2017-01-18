using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidORM.Data;
using RapidORM.Helpers;
using RapidORM.Attributes;
using RapidORM.Interfaces;
using RapidORM.Client.SQL;
using RapidORM.Common;
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

        private SqlEntity<SystemUser> dbEntity = null;

        public SystemUser()
        {
            dbEntity = new SqlEntity<SystemUser>();
        }

        public SystemUser(Dictionary<string, object> args)
        {
            Id = Convert.ToInt32(args["Id"].ToString());
            Name = args["Name"].ToString();
            Email = args["Email"].ToString();
            Password = args["Password"].ToString();
            DesignatedPosition = args["DesignatedPosition"].ToString();
        }

        #region Instance Methods
        public void Save(SystemUser user)
        {
            dbEntity.SaveChanges(user);            
        }

        public int InsertUserAndReturnAnId(SystemUser user)
        {
            int returnedId = dbEntity.InsertObjectAndReturnsId(user);

            return returnedId;
        }

        public void DeleteUserByPropertyName(string fieldValue)
        {
            dbEntity.DeleteObject(new SystemUser
            {
                Email = fieldValue
            }, "Email");
        }

        public void DeleteUserByObject(SystemUser user)
        {
            dbEntity.DeleteObject(user);
        }

        public IEnumerable<SystemUser> GetAllSystemUsers()
        {
            IEnumerable<SystemUser> systemUsers = dbEntity.GetAllObjects();

            return (systemUsers.Count() > 0) ? systemUsers : null;
        }

        public IEnumerable<SystemUser> GetUserByDesignatedPosition(string designatedPosition)
        {
            var systemUsers = dbEntity.GetObjectsByCriteria(new SearchCriteria
            {
                Column = PropertyHelper.GetPropertyName(() => this.DesignatedPosition),
                Value = designatedPosition
            });

            return (systemUsers.Count() > 0) ? systemUsers : null;
        }

        public IEnumerable<SystemUser> GetUsersByStringCriteria(int id)
        {
            var systemUsers = dbEntity.GetObjectsByCriteria("Id", id.ToString());

            return (systemUsers.Count() > 0) ? systemUsers : null;
        }

        public IEnumerable<SystemUser> GetUsersByMultipleCriteria(string name, string designatedPosition)
        {
            var systemUsers = dbEntity.GetObjectsByMultipleCriterias(new List<SearchCriteria> 
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
