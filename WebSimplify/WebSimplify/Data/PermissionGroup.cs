using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class PermissionGroup : IDbLoadable
    {
        public PermissionGroup()
        {

        }
        public PermissionGroup(IDataReader data)
        {
            Load(data);
        }

        public int Id { get;  set; }
        public string Name { get;  set; }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Name = DataAccessUtility.LoadNullable<string>(reader, "Name");
        }
    }
}