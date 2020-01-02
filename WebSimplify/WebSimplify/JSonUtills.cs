using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public static class JSonUtills
    {
        public static string ToJSonString(object obj)
        {
            return  JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static dynamic LoadJSon(string jsonString)
        {
            dynamic json = JValue.Parse(jsonString);
            return json;
        }

        public static T ParseJson<T>(string jsonString)
        {
            JObject jo = JObject.Parse(jsonString);
            return jo.ToObject<T>();
        }
    }
}