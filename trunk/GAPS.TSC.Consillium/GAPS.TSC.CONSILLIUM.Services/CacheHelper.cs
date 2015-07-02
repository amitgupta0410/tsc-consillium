using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace GAPS.TSC.CONSILLIUM.Services
{
    public class CacheHelper
    {
        private static readonly ObjectCache Cache;

        static CacheHelper()
        {
            Cache = MemoryCache.Default;
        }

        public static bool HasKey(string key)
        {
            return Cache[key] != null;
        }

        public static void Add(string key, object data)
        {
            Cache.Add(new CacheItem(key, data), new CacheItemPolicy { });
        }
        public static object Get(string key)
        {
            return Cache[key];
        }
        public static void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}
