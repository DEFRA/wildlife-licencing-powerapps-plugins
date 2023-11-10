using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin
{
    public static class ExceptionHandler
    {
        public static void SaveToTable(IOrganizationService service, Exception exc, string sdkMessage, string className)
        {
            service.Create(new Entity("sdds_failurelog")
            {
                ["sdds_errordetail"] = exc.Message + exc.StackTrace,
                ["sdds_event"] = sdkMessage,
                ["sdds_processname"] = className,
                ["sdds_source"] = new OptionSetValue(452120001)
            });
        }
    }
}
