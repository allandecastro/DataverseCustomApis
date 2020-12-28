using Dataverse.Shared;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace Dataverse.CustomApis
{
    public class EmailToTeam : PluginBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailToTeam" /> class
        /// </summary>
        public EmailToTeam() : base(typeof(EmailToTeam))
        {
            RegisteredEvents.Add(
                  new Tuple
                  <MessageProcessingStepStage, string, Action<LocalContext>>(
                       MessageProcessingStepStage.MainOperation, MessageName.dtv_EmailToTeam, Execute));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localContext"></param>
        public void Execute(LocalContext localContext)
        {
            string teamName = localContext.PluginExecutionContext.InputParameters["TeamName"] as string;
            EntityReference teamReference = localContext.PluginExecutionContext.InputParameters["Team"] as EntityReference;

            if ((!string.IsNullOrEmpty(teamName)
                || teamReference != null)
                    && localContext.PluginExecutionContext.InputParameters["Email"] is Entity emailEntity)
            {
                Guid teamId;
                if (teamReference != null)
                    teamId = teamReference.Id;
                else
                {
                    //Get Team
                    QueryExpression teamQuery = new QueryExpression()
                    {
                        EntityName = "team",
                        ColumnSet = new ColumnSet("teamid", "name"),
                    };

                    teamQuery.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Equal, teamName));
                    Entity teamEntity = localContext.OrganizationService.RetrieveMultiple(teamQuery).Entities.FirstOrDefault();
                    if (teamEntity != null)
                        teamId = teamEntity.Id;
                    else
                        throw new InvalidPluginExecutionException(OperationStatus.Failed, "The team could not be found.");
                }

                QueryExpression userQuery = new QueryExpression("systemuser")
                {
                    ColumnSet = new ColumnSet("systemuserid")
                };
                LinkEntity teamLink = new LinkEntity("systemuser", "teammembership", "systemuserid", "systemuserid", JoinOperator.Inner);
                ConditionExpression teamCondition = new ConditionExpression("teamid", ConditionOperator.Equal, teamId);
                teamLink.LinkCriteria.AddCondition(teamCondition);
                userQuery.LinkEntities.Add(teamLink);
                EntityCollection retrievedUsers = localContext.OrganizationService.RetrieveMultiple(userQuery);


                if (retrievedUsers.Entities.Count > 0)
                {
                    Entity emailEnt = new Entity("email", emailEntity.Id);

                    EntityCollection to = new EntityCollection();

                    foreach (Entity user in retrievedUsers.Entities)
                    {
                        Guid userId = user.Id;

                        Entity partyEntity = new Entity("activityparty");
                        partyEntity["partyid"] = new EntityReference("systemuser", userId);
                        to.Entities.Add(partyEntity);
                    }
                    emailEnt["to"] = to;
                    localContext.OrganizationService.Update(emailEnt);
                }
                else
                    throw new InvalidPluginExecutionException(OperationStatus.Failed, "The team has no members.");
            }
            else
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "InputParameters 'Email' and 'TeamName' or 'Team' are null or empty. At least one of them must be defined.");
        }
    }
}
