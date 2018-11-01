using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class QuickTask : IDbLoadable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public bool Active { get;  set; }
        public DateTime CreationDate { get;  set; }

        public QuickTask()
        {

        }
        public QuickTask(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            UserGroupId = DataAccessUtility.LoadInt32(reader, "UserGroupId");
            Name = DataAccessUtility.LoadNullable<string>(reader, "Name");
            Active = DataAccessUtility.LoadNullable<bool>(reader, "Active");
            CreationDate = DataAccessUtility.LoadNullable<DateTime>(reader, "CreationDate");
            Description = DataAccessUtility.LoadNullable<string>(reader, "Description");
        }
    }
}