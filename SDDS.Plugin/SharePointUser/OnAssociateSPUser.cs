using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SDDS.Plugin.Model;

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
            tracing.Trace("1.......");
            if (!context.InputParameters.Contains("Target") || !(context.InputParameters["Target"] is EntityReference)) return;
            tracing.Trace("2.......");
            if (!context.InputParameters.Contains("Relationship") || ((Relationship)context.InputParameters["Relationship"]).SchemaName != "teammembership_association") return;
            tracing.Trace("3.......");
            if (!context.InputParameters.Contains("RelatedEntities") && !(context.InputParameters["RelatedEntities"] is EntityReferenceCollection)) return;
            tracing.Trace("4.......");

            EntityReference target = (EntityReference)context.InputParameters["Target"];
            var teamMembersRefColl = (EntityReferenceCollection)context.InputParameters["RelatedEntities"];

            tracing.Trace("teamMembersRefColl retrieved: " + teamMembersRefColl.Count().ToString());

            Entity team = service.Retrieve(target.LogicalName, target.Id, new ColumnSet("sdds_adduserstosharepoint"));
            bool addUser = team.GetAttributeValue<bool>("sdds_adduserstosharepoint");

            tracing.Trace("Entering Logic.....");

            if (context.MessageName.ToLower() == "associate")
            {
                tracing.Trace("association");
                tracing.Trace(target.Id.ToString());
                
                if(addUser)
                {
                    var sp = CheckIfUserExist(teamMembersRefColl, service, tracing);
                    tracing.Trace(sp.Id.ToString());
                    if (sp.Id == Guid.Empty)
                    {
                        tracing.Trace("Creating Record");
                        var user = GetUserId(teamMembersRefColl, service, tracing);
                        if (user.Id != Guid.Empty)
                        {
                            Entity spUser = new Entity("sdds_sharepointuser");
                            spUser.Attributes["sdds_userid"] = new EntityReference("systemuser", user.Id);
                            spUser.Attributes["sdds_name"] = user.Name;
                            service.Create(spUser);
                        }
                    }else if(sp.RemoveUser == true)
                    {
                        tracing.Trace(sp.RemoveUser.ToString());
                        Entity sharePointUser = new Entity("sdds_sharepointuser");
                        sharePointUser.Id= sp.Id;
                        sharePointUser["sdds_removeuserfromsharepointgroup"] = false;
                        sharePointUser["sdds_sharepointuserid"] = sp.Id;
                        service.Update(sharePointUser);
                    }
                }
            }

            if (context.MessageName.ToLower() == "disassociate")
            {
                tracing.Trace("disassociation");
                if(GetUserTeams(teamMembersRefColl, service) && addUser)
                {
                    var sharePointUser = CheckIfUserExist(teamMembersRefColl, service, tracing);
                    if(sharePointUser.Id != Guid.Empty)
                    {
                        Entity sharePointUserEntity = new Entity("sdds_sharepointuser");
                        sharePointUserEntity.Id= sharePointUser.Id;
                        sharePointUserEntity["sdds_removeuserfromsharepointgroup"] = true;
                        sharePointUserEntity["sdds_sharepointuserid"] = sharePointUser.Id;
                        service.Update(sharePointUserEntity);
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

        private static Model.SharePointUser CheckIfUserExist(EntityReferenceCollection entityCollection, IOrganizationService service, ITracingService tracing)
        {
            var sp = new Model.SharePointUser();
            var entRef = entityCollection[0].Id;
            var query = new QueryExpression("sdds_sharepointuser");

            query.ColumnSet.AddColumns("sdds_name", "sdds_sharepointuserid", "sdds_removeuserfromsharepointgroup", "sdds_userid");
            query.Criteria.AddCondition("sdds_userid", ConditionOperator.Equal, entRef);

            var sharepointUser = service.RetrieveMultiple(query);
            if (sharepointUser.Entities.Count() > 0) {
                sp.Id = sharepointUser.Entities[0].GetAttributeValue<Guid>("sdds_sharepointuserid");
                sp.RemoveUser = sharepointUser.Entities[0].GetAttributeValue<bool>("sdds_removeuserfromsharepointgroup");
                sp.Name = sharepointUser.Entities[0].GetAttributeValue<string>("sdds_name");
                return sp; 
            } else return sp;

        }

        private static bool GetUserTeams(EntityReferenceCollection entityCollection, IOrganizationService service)
        {
            var entRefId = entityCollection[0].Id;

            var query = new QueryExpression("team");
            query.ColumnSet.AddColumns("description", "name", "teamtype", "teamid");
            query.Criteria.AddCondition("sdds_adduserstosharepoint", ConditionOperator.Equal, true);
            var query_teammembership = query.AddLink("teammembership", "teamid", "teamid");
            query_teammembership.LinkCriteria.AddCondition("systemuserid", ConditionOperator.Equal, entRefId);

            var userTeams = service.RetrieveMultiple(query);

            if (userTeams.Entities.Count() > 0)
             return false;
               else return true;

        }
    }

}
