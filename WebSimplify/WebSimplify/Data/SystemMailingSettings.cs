using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SystemMailingSettings : GenericData
    {

        public bool Exists { get; set; }
        [GenericDataField("ExistsText", "Exists")]
        public string ExistsText
        {
            get { return Exists.ToString(); }
            set { Exists = value.ToBoolean(); }
        }

        [GenericDataField("SystemEmailAddress", "SystemEmailAddress")]
        public string SystemEmailAddress { get;  set; }
        [GenericDataField("SystemName", "SystemName")]
        public string SystemName { get;  set; }
        [GenericDataField("NetworkCredentialPassword", "NetworkCredentialPassword")]
        public string NetworkCredentialPassword { get;  set; }
        [GenericDataField("NetworkCredentialUserName", "NetworkCredentialUserName")]
        public string NetworkCredentialUserName { get;  set; }
        [GenericDataField("EmailsGenericSubject", "EmailsGenericSubject")]
        public string EmailsGenericSubject { get;  set; }

        public SystemMailingSettings(IDataReader data)
        {
            Load(data);
        }

        public SystemMailingSettings()
        {

        }
    }
}