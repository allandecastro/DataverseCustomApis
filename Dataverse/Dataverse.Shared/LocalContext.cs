using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dataverse.Shared
{
    public class LocalContext : ILocalContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalContext" /> class
        /// </summary>
        /// <param name="serviceProvider">Dataverse service</param>
        internal LocalContext(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider)); //TR

            // Obtain the execution context service from the service provider.
            PluginExecutionContext =
                (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Obtain the tracing service from the service provider.
            TracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the Organization Service factory service from the service provider
            OrganizationServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            // Use the factory to generate the Organization Service.
            OrganizationService = OrganizationServiceFactory.CreateOrganizationService(PluginExecutionContext.UserId);

            // Use the factory to generate the Organization Service.
            AdminOrganizationService = OrganizationServiceFactory.CreateOrganizationService(null);

        }
        public IOrganizationServiceFactory OrganizationServiceFactory { get; }

        public IPluginExecutionContext PluginExecutionContext { get; }

        public IOrganizationService OrganizationService { get; set; }

        public IOrganizationService AdminOrganizationService { get; }

        public ITracingService TracingService { get; }
    }
}
