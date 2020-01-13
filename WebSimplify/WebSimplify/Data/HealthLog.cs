using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class HealthLog : GenericData
    {
        public int EscorterId { get; set; }
        [GenericDataField("EscorterIdText", "EscorterId")]
        public string EscorterIdText
        {
            get { return EscorterId.ToString(); }
            set { EscorterId = value.ToInteger(); }
        }

        public int ParentId { get; set; }
        [GenericDataField("ParentIdIdText", "ParentId")]
        public string ParentIdIdText
        {
            get { return ParentId.ToString(); }
            set { ParentId = value.ToInteger(); }
        }

        [GenericDataField("Title", "Title")]
        public string Title { get; set; }


        public DateTime IDate { get; set; }
        [GenericDataField("IDateText", "IDate")]
        public string IDateText
        {
            get { return IDate.ToString(); }
            set { IDate = value.ToDateTime(); }
        }

        internal override string FormatedGenericValue(string valueToFormat, GenericDataFieldAttribute genericFieldInfo, IDatabaseProvider db)
        {
            if (genericFieldInfo.PropertyName == "EscorterIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var u = db.DbAuth.GetUser(valueToFormat.ToInteger());
                    return u.DisplayName;
                }
            }
            if (genericFieldInfo.PropertyName == "ParentIdIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var u = db.DbGenericData.GetSingleGenericData(new GenericDataSearchParameters { Id = valueToFormat.ToInteger(), FromType = typeof(Parent) });
                    return (u as Parent).ParentName;
                }
            }
            return base.FormatedGenericValue(valueToFormat, genericFieldInfo, db);
        }


        public HealthLog(IDataReader data)
        {
            Load(data);
        }

        public HealthLog()
        {

        }
    }
}