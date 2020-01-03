using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public enum GenericDataEnum
    {
        UserAppPreferences
    }

    public class GenericData : IDbLoadable
    {
        public const string GenericDataExtraFieldPrefix = "ExtraField_";
        public const int GenericDataExtraFieldCount = 20;

        public virtual GenericDataEnum GenericDataType { get; }
        public int Id { get;  set; }
        public DateTime? UpdateDate { get;  set; }
        public DateTime CreationDate { get;  set; }
        public bool Active { get;  set; }
        public string Description { get;  set; }

         public GenericData()
        {
        }
        public GenericData(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");

            //GenericDataType = (GenericDataEnum)DataAccessUtility.LoadInt32(reader, "GenericDataType");
            CreationDate = DataAccessUtility.LoadNullable<DateTime>(reader, "CreationDate");
            UpdateDate = DataAccessUtility.LoadNullable<DateTime?>(reader, "UpdateDate");
            Active = DataAccessUtility.LoadNullable<bool>(reader, "Active");
            Description = DataAccessUtility.LoadNullable<string>(reader, "Description");

            LoadExtraFields(reader);
        }

        public virtual void LoadExtraFields(IDataReader reader)
        {
            
        }
    }
}