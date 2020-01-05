using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SystemMailingSettings : GenericData
    {
        public override GenericDataEnum GenericDataType => GenericDataEnum.SystemMailingSettings;

        public string SystemEmailAddress { get; internal set; }
        public string SystemName { get; internal set; }
        public string NetworkCredentialPassword { get; internal set; }
        public string NetworkCredentialUserName { get; internal set; }
        public string EmailsGenericSubject { get; internal set; }
    }
}