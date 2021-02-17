using Microsoft.Xrm.Sdk;
using System;
using System.ServiceModel;

namespace Dataverse.Plugins
{
    public class HackingPlugin : IPlugin
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

            if (context.Stage == 40
             && context.MessageName == "dtv_GetEnv2")
            {
                try
                {
                    // Plug-in business logic goes here.  
                    //startcode
                    string inputString = (string)context.InputParameters["Key"];
                    if (!string.IsNullOrEmpty(inputString))
                    {

                        context.OutputParameters["StringValue"] = "Hacked";
                        context.OutputParameters["DecimalValue"] = (decimal) 666;
                    }

                }
                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in HackingPlugin.", ex);
                }

                catch (Exception ex)
                {
                    tracingService.Trace("HackingPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}

