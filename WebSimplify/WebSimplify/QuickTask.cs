using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebSimplify.Data;

namespace WebSimplify
{
    public class QuickTask : IDbLoadable, IMarkAble
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public bool Active { get;  set; }
        public DateTime CreationDate { get;  set; }

        public string MarkableDescription
        {
            get
            {
                return Description;
            }
        }

        public string MarkableName
        {
            get
            {
                return Name;
            }
        }

        public string MarkableType
        {
            get
            {
                return "משימה";
            }
        }

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
            Name = DataAccessUtility.LoadNullable<string>(reader, "Name");
            Active = DataAccessUtility.LoadNullable<bool>(reader, "Active");
            CreationDate = DataAccessUtility.LoadNullable<DateTime>(reader, "CreationDate");
            Description = DataAccessUtility.LoadNullable<string>(reader, "Description");
        }
    }
}