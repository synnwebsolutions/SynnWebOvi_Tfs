using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class DevTaskItem : IDbLoadable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DevTaskStatus Status { get; set; }
        public int Id { get; set; }

        public DevTaskItem() 
        {

        }

        public DevTaskItem(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Status = (DevTaskStatus)DataAccessUtility.LoadInt32(reader, "Status");
            Name = DataAccessUtility.LoadNullable<string>(reader, "Name");
            Description = DataAccessUtility.LoadNullable<string>(reader, "Description");
        }
    }


    public enum DevTaskStatus
    {
        [Description("פתוח")]
        Open,
        [Description("בפיתוח")]
        InProgress,
        [Description("בוטל")]
        Canceled,
        [Description("בסביבת פיתוח")]
        OnTfs,
        [Description("בגרסה נוכחית")]
        OnServer,
        [Description("סגור בשרת ובסביבה")]
        Finalized
    }
}