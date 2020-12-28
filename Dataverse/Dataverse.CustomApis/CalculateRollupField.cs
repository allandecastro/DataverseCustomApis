using Dataverse.Shared;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Linq;

namespace Dataverse.CustomApis
{
    public class CalculateRollupField : PluginBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateRollupField" /> class
        /// </summary>
        public CalculateRollupField() : base(typeof(CalculateRollupField))
        {
            RegisteredEvents.Add(
                  new Tuple
                  <MessageProcessingStepStage, string, Action<LocalContext>>(
                       MessageProcessingStepStage.MainOperation, MessageName.dtv_CalculateRollupField, Execute));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localContext"></param>
        public void Execute(LocalContext localContext)
        {
            IOrganizationService service = localContext.OrganizationService;
            string fieldName = (string)localContext.PluginExecutionContext.InputParameters["FieldName"];

            if (!string.IsNullOrEmpty(fieldName)
                && localContext.PluginExecutionContext.InputParameters["Entity"] is Entity targetEntity)
            {
                CalculateRollupFieldRequest calculateRollup = new CalculateRollupFieldRequest
                {
                    FieldName = fieldName,
                    Target = new EntityReference(targetEntity.LogicalName, targetEntity.Id)
                };
                CalculateRollupFieldResponse resp = (CalculateRollupFieldResponse)service.Execute(calculateRollup);
                if (resp.Results.Values.FirstOrDefault() is Entity result)
                {
                    Type typeField = result[fieldName].GetType();
                    switch (typeField.Name)
                    {
                        case "Money":
                            localContext.PluginExecutionContext.OutputParameters["MoneyValue"] = result.GetAttributeValue<Money>(fieldName);
                            break;
                        case "DateTime":
                            localContext.PluginExecutionContext.OutputParameters["DateTimeValue"] = result.GetAttributeValue<DateTime>(fieldName);
                            break;
                        case "Int32":
                            localContext.PluginExecutionContext.OutputParameters["WholeNumberValue"] = result.GetAttributeValue<int>(fieldName);
                            break;
                        case "Decimal":
                            localContext.PluginExecutionContext.OutputParameters["DecimalValue"] = result.GetAttributeValue<decimal>(fieldName);
                            break;
                        default:
                            break;
                    }
                }

            }
            else
            {
                throw new InvalidPluginExecutionException(OperationStatus.Failed, "InputParameters 'FieldName' or 'Entity is null or empty.");
            }
        }

    }
}
