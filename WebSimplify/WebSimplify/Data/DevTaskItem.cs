using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using WebSimplify.Controls;

namespace WebSimplify
{
    public class DevTaskItem : IDbLoadable
    {
        [XEditor(Display = "שם", XControlType = XEditorControlType.Text)]
        public string Name { get; set; }
        [XEditor(Display = "תיאור", XControlType = XEditorControlType.TextArea)]
        public string Description { get; set; }

        [XEditor(Display = "סטטוס", XControlType = XEditorControlType.Enum, EnumType = typeof(DevTaskStatus))]
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
        [Description("בגרסה קרובה")]
        OnServer,
        [Description("סגור בשרת ובסביבה")]
        Finalized
    }
}