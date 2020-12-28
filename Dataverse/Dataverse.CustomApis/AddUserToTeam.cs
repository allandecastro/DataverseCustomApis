using Dataverse.Shared;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace Dataverse.CustomApis
{
    public class AddUserToTeam : PluginBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AddUserToTeam" /> class
        /// </summary>
        public AddUserToTeam() : base(typeof(AddUserToTeam))
        {
            RegisteredEvents.Add(
                  new Tuple
                  <MessageProcessingStepStage, string, Action<LocalContext>>(
                       MessageProcessingStepStage.MainOperation, MessageName.dtv_AddUserToTeam, Execute));
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

                QueryExpression teamQuery = new QueryExpression()
                {
                    EntityName = "team",
                    ColumnSet = new ColumnSet("teamid", "name"),
                };
                if (teamReference != null)
                    teamQuery.Criteria.AddCondition(new ConditionExpression("teamid", ConditionOperator.Equal, teamReference.Id));
                else
                    teamQuery.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Equal, teamName));
                Entity teamEntity = localContext.OrganizationService.RetrieveMultiple(teamQuery).Entities.FirstOrDefault();
                teamReference = teamEntity == null
                    ? throw new InvalidPluginExecutionException(OperationStatus.Failed, $"No team exists with the name: {teamName}.")
                    : teamEntity.ToEntityReference();

                AddMembersTeamRequest req = new AddMembersTeamRequest
                {
                    TeamId = teamReference.Id,
                    MemberIds = new[] { localContext.PluginExecutionContext.InputParameters["User"] is EntityReference userReference
                        ? userReference.Id
                        : localContext.PluginExecutionContext.InitiatingUserId }
                };
                localContext.OrganizationService.Execute(req);


            }
            else
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "InputParameters 'TeamName' or 'Team' is null or empty. At least one of them must be defined.");
        }
    }

}
