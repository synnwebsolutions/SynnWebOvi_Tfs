using Google.Apis.Json;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarUtilities
{
    public interface IGoogleDataStore
    {
        void ClearData();
        void DeleteStoredKey(string key);
        string GetUserCredentialsByKey(string key);
        void Upsert(string key, string serializedCredentials);
    }

    public class GoogleDatabaseDataStore : IDataStore
    {
        private IGoogleDataStore iData;
        public GoogleDatabaseDataStore(IGoogleDataStore idataStore)
        {
            iData = idataStore;
        }

        public Task ClearAsync()
        {
            iData.ClearData();
            return Task.Delay(0);
        }

        public Task DeleteAsync<T>(string key)
        {
            iData.DeleteStoredKey(GenerateStoredKey(key, typeof(T)));
            return Task.Delay(0);
        }

        public Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            string credentials = iData.GetUserCredentialsByKey(GenerateStoredKey(key, typeof(T)));
            if (credentials != null)
            {
                try
                {
                    tcs.SetResult(NewtonsoftJsonSerializer.Instance.Deserialize<T>(credentials));
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }
            else
            {
                tcs.SetResult(default(T));
            }
            return tcs.Task;
        }

        public Task StoreAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            var serialized = NewtonsoftJsonSerializer.Instance.Serialize(value);
            iData.Upsert(GenerateStoredKey(key, typeof(T)), serialized);
            return Task.Delay(0);
        }

        public string GenerateStoredKey(string key, Type t)
        {
            return string.Format("{0}-{1}", t.FullName, key);
        }
    }

}
