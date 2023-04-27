using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Model
{
    public class SharePointUser
    {
        public Guid Id { get; set; }
        public bool RemoveUser { get; set; }
        public string Name { get; set; }

        public EntityReference User { get; set; }
    }
}
