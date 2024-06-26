﻿using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Workflow
{
    public static class ExceptionHandler
    {
        public static void SaveToTable(IOrganizationService service, Exception exc, string sdkMessage, string className, int priority)
        {
            service.Create(new Entity("sdds_failurelog")
            {
                ["sdds_errordetail"] = exc.Message + exc.StackTrace,
                ["sdds_event"] = sdkMessage,
                ["sdds_processname"] = className,
                ["sdds_name"] = className,
                ["sdds_source"] = new OptionSetValue(452120003),
                ["sdds_priority"] = new OptionSetValue(priority)
            });
        }
    }
}
