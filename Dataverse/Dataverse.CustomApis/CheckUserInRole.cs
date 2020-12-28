using Dataverse.Shared;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace Dataverse.CustomApis
{
    public class CheckUserInRole : PluginBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckUserInRole" /> class
        /// </summary>
        public CheckUserInRole() : base(typeof(CheckUserInRole))
        {
            RegisteredEvents.Add(
                  new Tuple
                  <MessageProcessingStepStage, string, Action<LocalContext>>(
                       MessageProcessingStepStage.MainOperation, MessageName.dtv_CheckUserInRole, Execute));

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
                QueryExpression linkQuery = new QueryExpression()
                {
                    EntityName = "role",
                    ColumnSet = new ColumnSet("parentrootroleid", "name"),
                };

                if (roleReference != null)
                    linkQuery.Criteria.AddCondition(new ConditionExpression("parentrootroleid", ConditionOperator.Equal, roleReference.Id.ToString()));
                else
                    linkQuery.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Equal, roleName));

                LinkEntity systemUserLink = new LinkEntity("role", "systemuserroles", "roleid", "roleid", JoinOperator.Inner)
                {
                    Columns = new ColumnSet(false),
                    EntityAlias = "systemuserroles"
                };
                systemUserLink.LinkCriteria.AddCondition(new ConditionExpression("systemuserid", ConditionOperator.Equal, userId));
                linkQuery.LinkEntities.Add(systemUserLink);

                Entity matchEntity = localContext.OrganizationService.RetrieveMultiple(linkQuery).Entities.FirstOrDefault();
                if (matchEntity != null)
                    localContext.PluginExecutionContext.OutputParameters["IsUserInRole"] = true;
                else
                    localContext.PluginExecutionContext.OutputParameters["IsUserInRole"] = false;
            }
            else
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "InputParameters 'RoleName' or 'Role' is null or empty. At least one of them must be defined.");

        }
    }
}
