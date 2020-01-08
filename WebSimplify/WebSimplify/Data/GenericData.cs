using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebSimplify
{
    public class GenericData : IDbLoadable
    {
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

            var props = GetType().GetProperties();
            foreach (var pinfo in props)
            {
                var genericDataField = ((GenericDataFieldAttribute[])pinfo.GetCustomAttributes(typeof(GenericDataFieldAttribute), true)).FirstOrDefault();
                if (genericDataField != null)
                {
                    var dbValue = DataAccessUtility.LoadNullable<string>(reader, genericDataField.FieldName);
                    pinfo.SetValue(this, dbValue ?? string.Empty);
                }
            }
        }
    }

    public class GenericDataFieldAttribute: Attribute
    {
        public string FieldName { get; set; }
        public string PropertyName { get; set; }
        public bool DisableGridEdit { get; set; }
        public GenericDataFieldAttribute(string propName, string fieldName)
        {
            FieldName = fieldName;
            PropertyName = propName;
        }
    }
}