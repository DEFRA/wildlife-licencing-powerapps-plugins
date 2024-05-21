using Microsoft.Xrm.Sdk;
using SDDS.Plugin.EBG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.Model
{
    public class LicensableAction
    {
        public  sdds_licensableaction Action { get; set; }
        public EntityReferenceCollection Method { get; set; } = new EntityReferenceCollection();
    }
}
