using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class SystemMailingSettings : GenericData
    {
        public override GenericDataEnum GenericDataType => GenericDataEnum.SystemMailingSettings;

        public bool Exists { get; set; }
        public string SystemEmailAddress { get; internal set; }
        public string SystemName { get; internal set; }
        public string NetworkCredentialPassword { get; internal set; }
        public string NetworkCredentialUserName { get; internal set; }
        public string EmailsGenericSubject { get; internal set; }

        public override string GetGenericFieldValue(int i, ref bool addEmpty)
        {
            if (i == 0)
            {
                return StringCipher.Encrypt(SystemEmailAddress);
            }
            if (i == 1)
            {
                return SystemName;
            }
            if (i == 2)
            {
                return StringCipher.Encrypt(NetworkCredentialPassword);
            }
            if (i == 3)
            {
                return StringCipher.Encrypt(NetworkCredentialUserName);
            }
            if (i == 4)
            {
                return EmailsGenericSubject;
            }
            if (i == 5)
            {
                return Exists.ToString();
            }
            return base.GetGenericFieldValue(i, ref addEmpty);
        }

        public override void LoadGenericFieldValue(int i, string genericFieldDbValue)
        {
            if (i == 0)
            {
                SystemEmailAddress = StringCipher.Decrypt(genericFieldDbValue);
            }
            if (i == 1)
            {
                SystemName = genericFieldDbValue;
            }
            if (i == 2)
            {
                NetworkCredentialPassword = StringCipher.Decrypt(genericFieldDbValue);
            }
            if (i == 3)
            {
                NetworkCredentialUserName = StringCipher.Decrypt(genericFieldDbValue);
            }
            if (i == 4)
            {
                EmailsGenericSubject = genericFieldDbValue;
            }
            if (i == 5)
            {
                if(genericFieldDbValue.NotEmpty())
                Exists = genericFieldDbValue.ToBoolean();
            }
            base.LoadGenericFieldValue(i, genericFieldDbValue);
        }


        public SystemMailingSettings(IDataReader data)
        {
            Load(data);
        }

        public SystemMailingSettings()
        {

        }
    }
}