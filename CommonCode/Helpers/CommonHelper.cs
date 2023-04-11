using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Web;

namespace BuildingMaintenance.CommonCode.Helpers
{
    public static class CommonHelper
    {
        public static object GetDataTokenValueFromRequest(RouteData dataTokens, string key)
        {
            return dataTokens.DataTokens != null && dataTokens.DataTokens.Count > 0 && dataTokens.DataTokens.ContainsKey(key) ? dataTokens.DataTokens[key] : null;
        }

        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        public class Response
        {
            public bool Success { get; set; }
            public object ResponseData { get; set; }
            public string Message { get; set; }
        }
    }

}
