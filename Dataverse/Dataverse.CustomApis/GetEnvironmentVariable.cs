using Dataverse.Shared;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;

namespace Dataverse.CustomApis
{
    /// <summary>
    /// 
    /// </summary>
    public class GetEnvironmentVariable : PluginBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetEnvironmentVariable" /> class
        /// </summary>
        public GetEnvironmentVariable() : base(typeof(GetEnvironmentVariable))
        {
            RegisteredEvents.Add(
              new Tuple
              <MessageProcessingStepStage, string, Action<LocalContext>>(
                   MessageProcessingStepStage.MainOperation, MessageName.dtv_GetEnvironmentVariable, Execute));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="localContext"></param>
        public void Execute(LocalContext localContext)
        {
            IOrganizationService service = localContext.OrganizationService;
            string inputString = (string)localContext.PluginExecutionContext.InputParameters["Key"];

            if (!string.IsNullOrEmpty(inputString))
            {
                QueryExpression queryExpression = new QueryExpression("environmentvariabledefinition")
                {
                    ColumnSet = new ColumnSet("schemaname", "valueschema", "displayname", "type", "defaultvalue")
                };
                queryExpression.Criteria.AddCondition(new ConditionExpression("schemaname", ConditionOperator.Equal, inputString));
                LinkEntity linkEntity = new LinkEntity("environmentvariabledefinition", "environmentvariablevalue", "environmentvariabledefinitionid", "environmentvariabledefinitionid", JoinOperator.Inner)
                {
                    Columns = new ColumnSet("value"),
                    EntityAlias = "environmentvariablevalue"
                };
                linkEntity.LinkCriteria.AddCondition(new ConditionExpression("value", ConditionOperator.NotNull));
                queryExpression.LinkEntities.Add(linkEntity);

                Entity result = service.RetrieveMultiple(queryExpression).Entities.FirstOrDefault();
                if (result != null)
                {

                    OptionSetValue optionSetValueType = result.GetAttributeValue<OptionSetValue>("type");
                    if (optionSetValueType != null)
                    {
                        int type = optionSetValueType.Value;

                        object currentVal = result.GetAttributeValue<AliasedValue>("environmentvariablevalue.value") != null
                                    ? result.GetAttributeValue<AliasedValue>("environmentvariablevalue.value").Value
                                    : result.GetAttributeValue<object>("defaultvalue");
                        switch (type)
                        {
                            case 100000000: //String
                                localContext.PluginExecutionContext.OutputParameters["StringValue"] = currentVal;
                                break;
                            case 100000001: //Number
                                localContext.PluginExecutionContext.OutputParameters["DecimalValue"] = currentVal;
                                break;
                            case 100000002: //Boolean
                                localContext.PluginExecutionContext.OutputParameters["BooleanValue"] = currentVal;
                                break;
                            case 100000003: //JSON
                                localContext.PluginExecutionContext.OutputParameters["StringValue"] = currentVal;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    throw new InvalidPluginExecutionException(OperationStatus.Failed, $"Environment Variable undefined. Make sure the Key '{inputString}' is correct or that the record exists.");
                }

            }
            else
            {
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "The InputParameters 'Key' is null or empty.");
            }
        }
    }
}
