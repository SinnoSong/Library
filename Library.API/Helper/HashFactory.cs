using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Helper
{
    public static class HashFactory
    {
        internal static string GetHash(object entity)
        {
            string result;
            var json = JsonConvert.SerializeObject(entity);
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