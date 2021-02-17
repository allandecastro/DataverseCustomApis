using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.ServiceModel;

namespace Dataverse.CustomApis
{
    public class dtv_GetEnvironmentVariable : IPlugin
    {

        public void Execute(IServiceProvider serviceProvider)
        {

            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));
            // The InputParameters collection contains all the data passed in the message request.  
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.Stage == 30
             && (context.MessageName == "dtv_GetEnvironmentVariable"
             || context.MessageName== "dtv_GetEnv2"))
            {
                try
                {
                    string inputString = (string)context.InputParameters["Key"];

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
                                        context.OutputParameters["StringValue"] = currentVal;
                                        break;
                                    case 100000001: //Number
                                        context.OutputParameters["DecimalValue"] = currentVal;
                                        break;
                                    case 100000002: //Boolean
                                        context.OutputParameters["BooleanValue"] = currentVal;
                                        break;
                                    case 100000003: //JSON
                                        context.OutputParameters["StringValue"] = currentVal;
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
                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in dtv_GetEnvironmentVariable.", ex);
                }
                catch (Exception ex)
                {
                    tracingService.Trace("dtv_GetEnvironmentVariable: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
