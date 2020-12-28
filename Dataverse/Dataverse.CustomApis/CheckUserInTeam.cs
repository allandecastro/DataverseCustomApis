using Dataverse.Shared;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace Dataverse.CustomApis
{
    public class CheckUserInTeam : PluginBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckUserInTeam" /> class
        /// </summary>
        public CheckUserInTeam() : base(typeof(CheckUserInTeam))
        {
            RegisteredEvents.Add(
                  new Tuple
                  <MessageProcessingStepStage, string, Action<LocalContext>>(
                       MessageProcessingStepStage.MainOperation, MessageName.dtv_CheckUserInTeam, Execute));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localContext"></param>
        public void Execute(LocalContext localContext)
        {
            string teamName = localContext.PluginExecutionContext.InputParameters["TeamName"] as string;
            EntityReference teamReference = localContext.PluginExecutionContext.InputParameters["Team"] as EntityReference;


            if (!string.IsNullOrEmpty(teamName)
                || teamReference != null)
            {
                Guid userId = localContext.PluginExecutionContext.InputParameters["User"] is EntityReference userReference ? userReference.Id : localContext.PluginExecutionContext.InitiatingUserId;

                QueryExpression teamQuery = new QueryExpression()
                {
                    EntityName = "team",
                    ColumnSet = new ColumnSet("teamid", "name"),
                };
                if (teamReference != null)
                    teamQuery.Criteria.AddCondition(new ConditionExpression("teamid", ConditionOperator.Equal, teamReference.Id));
                else
                    teamQuery.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Equal, teamName));

                LinkEntity memberShipLink = new LinkEntity("team", "teammembership", "teamid", "teamid", JoinOperator.Inner);
                LinkEntity systemUserLink = new LinkEntity("teammembership", "systemuser", "systemuserid", "systemuserid", JoinOperator.Inner);
                systemUserLink.LinkCriteria.Conditions.Add(new ConditionExpression("systemuserid", ConditionOperator.Equal, userId));
                memberShipLink.LinkEntities.Add(systemUserLink);
                teamQuery.LinkEntities.Add(memberShipLink);

                Entity matchEntity = localContext.OrganizationService.RetrieveMultiple(teamQuery).Entities.FirstOrDefault();
                if (matchEntity != null)
                    localContext.PluginExecutionContext.OutputParameters["IsUserInTeam"] = true;
                else
                    localContext.PluginExecutionContext.OutputParameters["IsUserInTeam"] = false;

            }
            else
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "InputParameters 'TeamName' or 'Team' is null or empty. At least one of them must be defined.");

        }
    }
}
