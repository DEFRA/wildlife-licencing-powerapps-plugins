using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin
{
    internal static class DataverseExtensions
    {
        public static Entity ToEntity(this EntityReference entityRef, IOrganizationService svc, params string[] columns)
        {
            var columnSet = new ColumnSet(true);
            if (columns != null) columnSet = new ColumnSet(columns);
            return svc.Retrieve(entityRef.LogicalName, entityRef.Id, columnSet);
        }
    }
}
