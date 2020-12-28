using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dataverse.Shared
{
    public interface ILocalContext
    {
        IOrganizationService AdminOrganizationService { get; }
        IOrganizationService OrganizationService { get; }
        ITracingService TracingService { get; }
    }
}
