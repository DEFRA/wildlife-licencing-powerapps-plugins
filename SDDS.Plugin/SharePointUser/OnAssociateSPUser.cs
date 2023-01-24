using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDDS.Plugin.SharePointUser
{
    public class OnAssociateSPUser : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            tracing.Trace("Entering association between Team and team member");
            tracing.Trace("Message Name: "+ context.MessageName.ToLower());
            if (context.MessageName.ToLower() != "associate" && context.MessageName.ToLower() != "disassociate") return;
            tracing.Trace(context.MessageName.ToLower());
            if (!context.InputParameters.Contains("Target") || !(context.InputParameters["Target"] is EntityReference)) return;
            if (!context.InputParameters.Contains("Relationship") && ((Relationship)context.InputParameters["Relationship"]).SchemaName != "teammembership_association") return;
            if (!context.InputParameters.Contains("RelatedEntities") && !(context.InputParameters["RelatedEntities"] is EntityReferenceCollection)) return;

            EntityReference target = (EntityReference)context.InputParameters["Target"];
            var teamMembersRefColl = (EntityReferenceCollection)context.InputParameters["RelatedEntities"];

            tracing.Trace("teamMembersRefColl retrieved: " + teamMembersRefColl.Count().ToString());

            tracing.Trace("Creating guid");
            List<Guid> teamIds = new List<Guid>()
            {
                new Guid("91bef0ff-640c-ed11-b83d-002248c5c45b"),
                new Guid("9ea9c408-8e77-ec11-8d21-000d3a87431b"),
                new Guid("090c101d-5f0c-ed11-b83d-002248c5c45b"),
                new Guid("ca30e88e-7f0b-ed11-b83d-002248c5c45b"),
                new Guid("a3e42f5c-2651-ec11-8f8e-000d3a0ce458"),
                new Guid("70891a2c-810b-ed11-b83d-002248c5c45b"),
                new Guid("dec31450-810b-ed11-b83d-002248c5c45b"),
                new Guid("3b662452-7e0b-ed11-b83d-002248c5c45b"),
                new Guid("dc46b306-9687-ec11-93b0-0022481b40f8")
            };

            if (context.MessageName.ToLower() == "associate")
            {
                tracing.Trace("association");
                if (teamIds.Contains(target.Id) && CheckIfUserExist(teamMembersRefColl, service) != Guid.Empty)
                {
                    var user = GetUserId(teamMembersRefColl, service, tracing);
                    if (user != null)
                    {
                        Entity spUser = new Entity("sdds_sharepointuser");
                        spUser.Attributes["sdds_userid"] = new EntityReference("systemuser", user.Id);
                        spUser.Attributes["sdds_name"] = user.Name;
                        service.Create(spUser);
                    }
                }
            }

            if (context.MessageName.ToLower() == "disassociate")
            {
                tracing.Trace("disassociation");
                if(GetUserTeams(teamMembersRefColl, service, teamIds) && teamIds.Contains(target.Id))
                {
                    var sharePointUserId = CheckIfUserExist(teamMembersRefColl, service);
                    if(sharePointUserId != Guid.Empty)
                    {
                        service.Delete("sdds_sharepointuser", sharePointUserId);
                    }
                }
            }

        }

        private static User GetUserId(EntityReferenceCollection entityCollection, IOrganizationService service, ITracingService tracing)
        {
            
            var entRef = entityCollection[0];
            User user = new User();

            var retrievedUser = service.Retrieve(entRef.LogicalName, entRef.Id, new Microsoft.Xrm.Sdk.Query.ColumnSet(new string[] { "systemuserid", "fullname" }));
            if (retrievedUser != null)
            {
                user.Id = retrievedUser.GetAttributeValue<Guid>("systemuserid");
                user.Name = retrievedUser.GetAttributeValue<string>("fullname");
            }

            return user;
        }

        private static Guid CheckIfUserExist(EntityReferenceCollection entityCollection, IOrganizationService service)
        {
            var entRef = entityCollection[0].Id;
            var query = new QueryExpression("sdds_sharepointuser");

            query.ColumnSet.AddColumns("sdds_name", "sdds_sharepointuserid");
            query.Criteria.AddCondition("sdds_userid", ConditionOperator.Equal, entRef);

            var sharepointUser = service.RetrieveMultiple(query);
            if (sharepointUser.Entities.Count() > 0) { return sharepointUser.Entities[0].GetAttributeValue<Guid>("sdds_sharepointuserid"); } else return Guid.Empty;

        }

        private static bool GetUserTeams(EntityReferenceCollection entityCollection, IOrganizationService service, List<Guid> teams)
        {
            var entRefId = entityCollection[0].Id;

            var query = new QueryExpression("team");
            query.ColumnSet.AddColumns("description", "name", "teamtype", "teamid");
            var query_teammembership = query.AddLink("teammembership", "teamid", "teamid");
            query_teammembership.LinkCriteria.AddCondition("systemuserid", ConditionOperator.Equal, entRefId);

            var userTeams = service.RetrieveMultiple(query);

            if (userTeams.Entities.Count() > 0 && userTeams.Entities.Any(x => teams.Contains(x.GetAttributeValue<Guid>("teamid"))))
             return false;
               else return true;

        }
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
