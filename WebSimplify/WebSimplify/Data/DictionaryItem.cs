using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace SynnWebOvi
{
    public class DictionaryItem : IDbLoadable
    {
        public string DictionaryKey { get;  set; }
        public string DictionaryValue { get;  set; }
        public int Id { get;  set; }
        public int UserGroupId { get;  set; }
        public DictionaryItem()
        {

        }
        public DictionaryItem(IDataReader data)
        {
            Load(data);
        }
        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            DictionaryKey = DataAccessUtility.LoadNullable<string>(reader, "dKey");
            DictionaryValue = DataAccessUtility.LoadNullable<string>(reader, "Value");
        }
    }
}