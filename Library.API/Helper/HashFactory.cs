using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Library.API.Helper
{
    public static class HashFactory
    {
        internal static string GetHash(object entity)
        {
            string result;
            // 配置忽略引用的集合属性
            var json = JsonConvert.SerializeObject(entity, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            var bytes = Encoding.UTF8.GetBytes(json);
            using (var hasher = MD5.Create())
            {
                var hash = hasher.ComputeHash(bytes);
                result = BitConverter.ToString(hash);
                result = result.Replace("-", "");
            }
            return result;
        }
    }
}