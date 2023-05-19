using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin
{
    public class HandleJson
    {
        public string Serialize<T>(T classObj) where T : class, new()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serialiaze = new DataContractJsonSerializer(typeof(T));
                serialiaze.WriteObject(ms, classObj);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        public T DeSerialize<T>(string classObj) where T : class, new()
        {
            DataContractJsonSerializer deSerialiaze = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(classObj)))
            {
                return deSerialiaze.ReadObject(ms) as T;
            }

        }
    }
}
