using Dataverse.Shared;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace Dataverse.CustomApis
{
    public class AddRoleToUser : PluginBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRoleToUser" /> class
        /// </summary>
        public AddRoleToUser() : base(typeof(AddRoleToUser))
        {
            RegisteredEvents.Add(
                  new Tuple
                  <MessageProcessingStepStage, string, Action<LocalContext>>(
                       MessageProcessingStepStage.MainOperation, MessageName.dtv_AddRoleToUser, Execute));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localContext"></param>
        public void Execute(LocalContext localContext)
        {
            string roleName = localContext.PluginExecutionContext.InputParameters["RoleName"] as string;
            EntityReference roleReference = localContext.PluginExecutionContext.InputParameters["Role"] as EntityReference;

            if (!string.IsNullOrEmpty(roleName)
                || roleReference != null)
            {
                Guid userId = localContext.PluginExecutionContext.InputParameters["User"] is EntityReference userReference ? userReference.Id : localContext.PluginExecutionContext.InitiatingUserId;

                Entity systemUser = localContext.OrganizationService.Retrieve("systemuser", userId, new ColumnSet("businessunitid"));
                EntityReference businessUnit = systemUser.GetAttributeValue<EntityReference>("businessunitid");

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
                    EntityReference entRootRole = (EntityReference)targetRole.Attributes["parentrootroleid"];

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
                                Values = { entRootRole.Id}
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
                                  "systemuser",
                                  userId,
                                  new Relationship("systemuserroles_association"),
                                  new EntityReferenceCollection() { new EntityReference("role", roleId) });
                    }
                    else
                        throw new InvalidPluginExecutionException(OperationStatus.Failed, "");

                }
                else
                    throw new InvalidPluginExecutionException(OperationStatus.Failed, "");


            }
            else
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "InputParameters 'RoleName' or 'Role' is null or empty. At least one of them must be defined.");

        }
    }
}
