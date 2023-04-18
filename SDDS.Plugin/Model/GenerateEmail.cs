using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Model
{
    [DataContract]
    public class GenerateEmail
    {
        [DataMember]
        public string guid { get; set; }
        [DataMember]
        public bool Granted { get; set; }
        [DataMember]
        public string Source { get; set; }
    }
}
