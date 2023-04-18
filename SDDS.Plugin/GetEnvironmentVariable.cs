using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin
{
    public static class GetEnvironmentVariable
    {
        public static string GetEnvValue(string schemaName, IOrganizationService service)
        {
            string value = string.Empty;
            // Set Condition Values
            var query_schemaname = schemaName; // "sdds_emailwithattachmentapiurl";

            // Instantiate QueryExpression query
            var query = new QueryExpression("environmentvariabledefinition");

            // Add columns to query.ColumnSet
            query.ColumnSet.AddColumns(
                "statecode",
                "environmentvariabledefinitionidunique",
                "schemaname",
                "environmentvariabledefinitionid",
                "displayname",
                "valueschema",
                "defaultvalue"
            );

            // Add filter query.Criteria
            query.Criteria.AddCondition("schemaname", ConditionOperator.Equal, query_schemaname);

            // Add link-entity aId
            var aId = query.AddLink("environmentvariablevalue", "environmentvariabledefinitionid", "environmentvariabledefinitionid");
            aId.EntityAlias = "aId";

            // Add columns to aId.Columns
            aId.Columns.AddColumn("value");

           var entityDef =  service.RetrieveMultiple(query);
            if (entityDef.Entities.Count() > 0)
            {
                var envVar = entityDef.Entities[0];
                value = envVar.GetAttributeValue<AliasedValue>("aId.value").Value.ToString() ?? envVar.GetAttributeValue<string>("defaultvalue");
            }

            return value;
        }
    }
}
