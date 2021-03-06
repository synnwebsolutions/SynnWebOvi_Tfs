﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SystemMailingSettings : GenericData
    {

        [GenericDataField("SystemEmailAddress", "SystemEmailAddress", DisableGridEdit = true)]
        public string SystemEmailAddress { get;  set; }
        [GenericDataField("SystemName", "SystemName")]
        public string SystemName { get;  set; }
        [GenericDataField("NetworkCredentialPassword", "NetworkCredentialPassword", DisableGridEdit = true)]
        public string NetworkCredentialPassword { get;  set; }
        [GenericDataField("NetworkCredentialUserName", "NetworkCredentialUserName", DisableGridEdit = true)]
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