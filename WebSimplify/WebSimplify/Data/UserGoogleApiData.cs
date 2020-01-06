using Newtonsoft.Json.Linq;
using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class UserGoogleApiData : GenericData
    {
        public Installed installed { get; set; }

        public override GenericDataEnum GenericDataType
        {
            get
            {
                return GenericDataEnum.UserGoogleApiSettings;
            }
        }
        public override string GetGenericFieldValue(int i, ref bool addEmpty)
        {
            if (installed == null)
                installed = new Installed();
            if (i == 0)
            {
                return UserId.ToString();
            }
            if (i == 1)
            {
                return StringCipher.Encrypt(installed.project_id);
            }
            if (i == 2)
            {
                return StringCipher.Encrypt(installed.client_id);
            }
            if (i == 3)
            {
                return StringCipher.Encrypt(installed.client_secret);
            }
            return base.GetGenericFieldValue(i, ref addEmpty);
        }

        public override void LoadGenericFieldValue(int i, string genericFieldDbValue)
        {
            if (installed == null)
                installed = new Installed();
            if (i == 0)
            {
                UserId = genericFieldDbValue.ToInteger();
            }
            if (i == 1)
            {
                if(!string.IsNullOrEmpty(genericFieldDbValue))
                    installed.project_id = StringCipher.Decrypt(genericFieldDbValue);
            }
            if (i == 2)
            {
                if (!string.IsNullOrEmpty(genericFieldDbValue))
                    installed.client_id = StringCipher.Decrypt(genericFieldDbValue);
            }
            if (i == 3)
            {
                if (!string.IsNullOrEmpty(genericFieldDbValue))
                    installed.client_secret = StringCipher.Decrypt(genericFieldDbValue);
            }
            
            base.LoadGenericFieldValue(i, genericFieldDbValue);
        }
      
        public UserGoogleApiData(IDataReader data)
        {
            Init();
            Load(data);
        }

        public int UserId { get; set; }
        public bool HasData
        {
            get
            {
                return true;
            }
        }

        public UserGoogleApiData()
        {
            Init();
        }

        private void Init()
        {
            if (installed == null)
                installed = new Installed
                {
                    auth_provider_x509_cert_url = "https://www.googleapis.com/oauth2/v1/certs",
                    auth_uri = "https://accounts.google.com/o/oauth2/auth",
                    token_uri = "https://oauth2.googleapis.com/token",
                    redirect_uris = new string[] { "urn:ietf:wg:oauth:2.0:oob", "http://localhost:63516//Diary.aspx" }
                };
        }

        internal string GenerateJsonString()
        {
            dynamic jo = new JObject();
            var insJ = JObject.FromObject(installed);
            jo.installed = insJ;
            var jstr = JSonUtills.ToJSonString(jo);

            return jstr;
        }
    }

    [Serializable]
    public class Installed
    {
        public string client_id { get; set; }
        public string project_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_secret { get; set; }
        public string[] redirect_uris { get; set; }
    }

}