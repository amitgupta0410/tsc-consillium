using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace GAPS.TSC.CONS.Services{
    public static class RestService{
        private static readonly string ApiBaseUrl = ConfigurationManager.AppSettings["ApiUrl"];
        private static readonly string ApiKey = ConfigurationManager.AppSettings["ApiKey"];

        public static T Get<T>(string url, IEnumerable<KeyValuePair<string, string>> data = null) where T : class, new() {
            if (CacheHelper.HasKey(url)) {
                var str = CacheHelper.Get(url) as T;
                if (str != null) {
                    return str;
                }
            }
            var client = new RestClient(ApiBaseUrl);
            var req = new RestRequest(url, Method.GET);
            req.AddParameter("key", ApiKey);
            if (null != data) {
                data.ToList().ForEach(x => req.AddParameter(x.Key, x.Value));
            }
            var response = client.Execute(req);

            var toReturn = JsonConvert.DeserializeObject<T>(response.Content);
            if (toReturn != null) {
                CacheHelper.Add(url, toReturn);
            }
            return toReturn;
        }

        public static string GetString(string url, IEnumerable<KeyValuePair<string, string>> data = null) {
            var client = new RestClient(ApiBaseUrl);
            var req = new RestRequest(url, Method.GET);
            req.AddParameter("key", ApiKey);
            if (null != data) {
                data.ToList().ForEach(x => req.AddParameter(x.Key, x.Value));
            }
            var response2 = client.Execute(req);
            return response2.Content;
        }

        public static bool Post(string url, IEnumerable<KeyValuePair<string, string>> data) {
            var client = new RestClient(ApiBaseUrl);
            var req = new RestRequest(url, Method.POST);
            req.AddQueryParameter("key", ApiKey);
            if (data != null) {
                data.ToList().ForEach(x => req.AddParameter(x.Key, x.Value));
            }
            var response2 = client.Execute(req);
            return response2.StatusCode == HttpStatusCode.OK;
        }

        public static bool Post<T>(string url, IEnumerable<KeyValuePair<string, string>> data, out T output)
            where T : new() {
            var client = new RestClient(ApiBaseUrl);
            var req = new RestRequest(url, Method.POST);
            req.AddQueryParameter("key", ApiKey);
            if (data != null) {
                data.ToList().ForEach(x => req.AddParameter(x.Key, x.Value));
            }
            var response2 = client.Execute<T>(req);
            output = response2.Data;
            return response2.StatusCode == HttpStatusCode.OK;
        }

        public static bool Post(string url, Object obj) {
            return Post(url, obj.ToDictionary<string>());
        }
    }

    public static class ObjectToDictionaryHelper{
        public static IDictionary<string, object> ToDictionary(this object source) {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source) {
            if (source == null) {
                //   ThrowExceptionWhenSourceArgumentIsNull();
                return new Dictionary<string, T>();
            }
            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source)) {
                AddPropertyToDictionary<T>(property, source, dictionary);
            }
            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source,
            Dictionary<string, T> dictionary) {
            object value = property.GetValue(source);
            if (IsOfType<T>(value)) {
                dictionary.Add(property.Name, (T) value);
            }
        }

        private static bool IsOfType<T>(object value) {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull() {
            throw new ArgumentNullException("source",
                "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}