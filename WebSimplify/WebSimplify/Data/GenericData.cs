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
        UserAppPreferences,
        UserGoogleApiSettings,
        CalendarBackgroundWorkerLog,
        CalendarJob,
        SystemMailingSettings,
        UserMemoSharingSettings
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

            CreationDate = DataAccessUtility.LoadNullable<DateTime>(reader, "CreationDate");
            UpdateDate = DataAccessUtility.LoadNullable<DateTime?>(reader, "UpdateDate");
            Active = DataAccessUtility.LoadNullable<bool>(reader, "Active");
            Description = DataAccessUtility.LoadNullable<string>(reader, "Description");

            for (int i = 0; i < GenericDataExtraFieldCount; i++)
            {
                var key = i.ApplyGenericDataPrefix();
                var genericFieldValue = DataAccessUtility.LoadNullable<string>(reader, key);
                LoadGenericFieldValue(i, genericFieldValue);
            }
        }

        public virtual void LoadGenericFieldValue(int index, string genericFieldDbValue)
        {
            
        }

        public void AppendExtraFieldsValues(List<KeyValuePair<int, object>> extraFields)
        {
            for (int i = 0; i < GenericDataExtraFieldCount; i++)
            {
                bool addEmpty = false;
                string genericFieldValue = GetGenericFieldValue(i, ref addEmpty);
                if (genericFieldValue != null || addEmpty)
                {
                    extraFields.Add(new KeyValuePair<int, object>(i, genericFieldValue ?? string.Empty));
                }
            }
        }

        public virtual string GetGenericFieldValue(int i, ref bool addEmpty)
        {
            return null;
        }
    }
}