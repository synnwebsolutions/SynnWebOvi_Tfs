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

        [GenericDataField("UserIdText", "UserId")]
        public string UserIdText
        {
            get { return UserId.ToString(); }
            set { UserId = value.ToInteger(); }
        }

        [GenericDataField("ProjectIdText", "ProjectId")]
        public string ProjectIdText
        {
            get { return  StringCipher.Encrypt(installed.project_id); }
            set { installed.project_id = StringCipher.Decrypt(value); }
        }

        [GenericDataField("ClientSecretText", "ClientSecret")]
        public string ClientSecretText
        {
            get { return StringCipher.Encrypt(installed.client_secret); }
            set { installed.client_secret = StringCipher.Decrypt(value); }
        }

        [GenericDataField("ClientIdText", "ClientId")]
        public string ClientIdText
        {
            get { return StringCipher.Encrypt(installed.client_id); }
            set { installed.client_id = StringCipher.Decrypt(value); }
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