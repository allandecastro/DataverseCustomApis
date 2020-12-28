using Dataverse.Shared;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataverse.CustomApis
{
    public class AddRoleToTeam : PluginBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoleToTeam" /> class
        /// </summary>
        public AddRoleToTeam() : base(typeof(AddRoleToTeam))
        {
            RegisteredEvents.Add(
                  new Tuple
                  <MessageProcessingStepStage, string, Action<LocalContext>>(
                       MessageProcessingStepStage.MainOperation, MessageName.dtv_AddRoleToTeam, Execute));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localContext"></param>
        public void Execute(LocalContext localContext)
        {
            string roleName = localContext.PluginExecutionContext.InputParameters["RoleName"] as string;
            EntityReference roleReference = localContext.PluginExecutionContext.InputParameters["Role"] as EntityReference;

            string teamName = localContext.PluginExecutionContext.InputParameters["TeamName"] as string;
            EntityReference teamReference = localContext.PluginExecutionContext.InputParameters["Team"] as EntityReference;

            if ((!string.IsNullOrEmpty(roleName)
                || roleReference != null)
                &&
                (!string.IsNullOrEmpty(teamName)
                || teamReference != null))
            {
                //Get Team
                QueryExpression teamQuery = new QueryExpression()
                {
                    EntityName = "team",
                    ColumnSet = new ColumnSet("teamid", "name", "businessunitid"),
                };
                if (teamReference != null)
                    teamQuery.Criteria.AddCondition(new ConditionExpression("teamid", ConditionOperator.Equal, teamReference.Id));
                else
                    teamQuery.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Equal, teamName));
                Entity teamEntity = localContext.OrganizationService.RetrieveMultiple(teamQuery).Entities.FirstOrDefault();
                if (teamEntity != null)
                {
                    teamReference = teamEntity.ToEntityReference();
                    EntityReference businessUnit = teamEntity.GetAttributeValue<EntityReference>("businessunitid");
                    //Get role.
                    QueryExpression query = new QueryExpression
                    {
                        EntityName = "role",
                        ColumnSet = new ColumnSet("parentrootroleid"),

                    };
                    if (roleReference != null)
                        query.Criteria.AddCondition(new ConditionExpression("roleid", ConditionOperator.Equal, roleReference.Id));
                    else
                        query.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Equal, roleName));

                    Entity targetRole = localContext.OrganizationService.RetrieveMultiple(query).Entities.FirstOrDefault();

                    if (targetRole != null)
                    {
                        EntityReference rootRoleReference = (EntityReference)targetRole.Attributes["parentrootroleid"];

                        QueryExpression query2 = new QueryExpression
                        {
                            EntityName = "role",
                            ColumnSet = new ColumnSet("roleid"),
                            Criteria = new FilterExpression
                            {
                                Conditions =
                        {

                            new ConditionExpression
                            {
                                AttributeName = "parentrootroleid",
                                Operator = ConditionOperator.Equal,
                                Values = { rootRoleReference.Id}
                            },
                            new ConditionExpression
                            {
                                AttributeName = "businessunitid",
                                Operator = ConditionOperator.Equal,
                                Values = { businessUnit.Id}
                            }
                        }
                            }
                        };

                        Entity securityRole = localContext.OrganizationService.RetrieveMultiple(query2).Entities.FirstOrDefault();
                        if (securityRole != null)
                        {
                            Guid roleId = (Guid)securityRole.Attributes["roleid"];

                            localContext.OrganizationService.Associate(
                              "team",
                              teamReference.Id,
                              new Relationship("teamroles_association"),
                              new EntityReferenceCollection() { new EntityReference("role", roleId) });
                        }
                        else
                            throw new InvalidPluginExecutionException(OperationStatus.Failed, $"Security role for the business unit {businessUnit.Name} not found.");

                    }
                    else
                        throw new InvalidPluginExecutionException(OperationStatus.Failed, "Security role not found.");

                }
                else
                    throw new InvalidPluginExecutionException(OperationStatus.Failed, "Team record not found.");
            }
            else
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "InputParameters 'RoleName' or 'Role' and 'TeamName' or 'Team' are null or empty. At least one of them must be defined.");

        }


    }
}
