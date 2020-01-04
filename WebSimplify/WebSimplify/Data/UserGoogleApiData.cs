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

        public override void AppendExtraFieldsValues(List<KeyValuePair<int, object>> extraFields)
        {
            extraFields.Add(new KeyValuePair<int, object>(0, UserId.ToString()));
            if (installed == null)
                installed = new Installed();
            extraFields.Add(new KeyValuePair<int, object>(1, installed.project_id));
            extraFields.Add(new KeyValuePair<int, object>(2, installed.client_id));
            extraFields.Add(new KeyValuePair<int, object>(3, installed.client_secret));
        }

        public override void LoadExtraFields(IDataReader reader)
        {
            if (installed == null)
                installed = new Installed();
            UserId = DataAccessUtility.LoadNullable<string>(reader, 0.ApplyGenericDataPrefix()).ToInteger();
            installed.project_id = DataAccessUtility.LoadNullable<string>(reader, 1.ApplyGenericDataPrefix());
            installed.client_id = DataAccessUtility.LoadNullable<string>(reader, 2.ApplyGenericDataPrefix());
            installed.client_secret = DataAccessUtility.LoadNullable<string>(reader, 3.ApplyGenericDataPrefix());
        }
        public UserGoogleApiData(IDataReader data)
        {
            Load(data);
        }

        public int UserId { get; set; }

        public UserGoogleApiData()
        {
            installed = new Installed
            {
                auth_provider_x509_cert_url = "https://www.googleapis.com/oauth2/v1/certs",
                auth_uri = "https://accounts.google.com/o/oauth2/auth",
                token_uri = "https://oauth2.googleapis.com/token",
                redirect_uris = new string[] { "urn:ietf:wg:oauth:2.0:oob", "http://localhost" }
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