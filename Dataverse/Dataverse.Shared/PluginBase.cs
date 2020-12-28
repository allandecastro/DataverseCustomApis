using Microsoft.Xrm.Sdk;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel;


namespace Dataverse.Shared
{
    public class PluginBase : IPlugin
    {
        private
            Collection
            <Tuple<MessageProcessingStepStage, string, Action<LocalContext>>>
            _registeredEvents;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PluginBase" /> class.
        /// </summary>
        /// <param name="childClassName">The <see cref="Type" /> of the derived class.</param>
        internal PluginBase(Type childClassName)
        {
            ChildClassName = childClassName.ToString();
        }

        /// <summary>
        ///     Gets the List of events that the plug-in should fire for. Each List
        ///     Item is a <see cref="System.Tuple" /> containing the Pipeline Mode, Stage, Message and (optionally) the Primary
        ///     Entity.
        ///     In addition, the last parameter provide the delegate to invoke on a matching registration.
        /// </summary>
        internal
            Collection
            <Tuple<MessageProcessingStepStage, string,  Action<LocalContext>>>
            RegisteredEvents
        {
            get
            {
                if (_registeredEvents == null)
                    _registeredEvents =
                        new Collection
                        <
                            Tuple
                            < MessageProcessingStepStage, string,
                                Action<LocalContext>>>();

                return _registeredEvents;
            }
        }

        /// <summary>
        ///     Gets or sets the name of the child class.
        /// </summary>
        /// <value>The name of the child class.</value>
        protected string ChildClassName { get; }


        /// <summary>
        ///     Executes the plug-in.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <remarks>
        ///     For improved performance, Microsoft Dynamics CRM caches plug-in instances.
        ///     The plug-in's Execute method should be written to be stateless as the constructor
        ///     is not called for every invocation of the plug-in. Also, multiple system threads
        ///     could execute the plug-in at the same time. All per invocation state information
        ///     is stored in the context. This means that you should not use global variables in plug-ins.
        /// </remarks>
        public void Execute(IServiceProvider serviceProvider)
        {

            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));

            // Construct the Local plug-in context.
            LocalContext localcontext = new LocalContext(serviceProvider);

            localcontext.TracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Entered {0}.Execute()", ChildClassName));

            try
            {
                // Iterate over all of the expected registered events to ensure that the plugin
                // has been invoked by an expected event
                // For any given plug-in event at an instance in time, we would expect at most 1 result to match.
                Action<LocalContext> entityAction =
                (from a in RegisteredEvents
                 where ((int)a.Item1 == localcontext.PluginExecutionContext.Stage) &&
                       (a.Item2 == localcontext.PluginExecutionContext.MessageName) 
                 select a.Item3).FirstOrDefault();

                if (entityAction != null)
                {
                    localcontext.TracingService.Trace(string.Format(
                        CultureInfo.InvariantCulture,
                        "{0} is firing for Entity: {1}, Message: {2}",
                        ChildClassName,
                        localcontext.PluginExecutionContext.PrimaryEntityName,
                        localcontext.PluginExecutionContext.MessageName));

                    entityAction.Invoke(localcontext);

                    // now exit - if the derived plug-in has incorrectly registered overlapping event registrations,
                    // guard against multiple executions.
                }
            }
            catch (InvalidPluginExecutionException e)
            {
                localcontext.TracingService.Trace(string.Format(CultureInfo.InvariantCulture, "InvalidPluginExecutionException: {0}", e));

                throw new InvalidPluginExecutionException(e.Message);
            }
            catch (FaultException<OrganizationServiceFault> e)
            {
                localcontext.TracingService.Trace(string.Format(CultureInfo.InvariantCulture, "FaultException: {0}", e));
                localcontext.TracingService.Trace("Detail.Message: " + e.Detail.Message);
                var innerFault = e.Detail.InnerFault;
                if (innerFault != null)
                    localcontext.TracingService.Trace("Detail.InnerFault: " + innerFault + "\n" + innerFault.Message + "\n" +
                                       innerFault.TraceText);
                throw new InvalidPluginExecutionException(e.Detail.Message);
            }
            catch (Exception e)
            {
                localcontext.TracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Exception: {0}", e));

                throw new InvalidPluginExecutionException(e.Message);
            }
            finally
            {
                localcontext.TracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Exiting {0}.Execute()", ChildClassName));
            }
        }
    }


    /// <summary>
    ///     The stage in the execution pipeline that a custom api is registered for.
    /// </summary>
    public enum MessageProcessingStepStage
    {
        MainOperation = 30,
    }
    /// <summary>
    ///     The names of the message that is being processed by the event execution pipeline.
    /// </summary>
    public static class MessageName
    {
        public const string dtv_AddRoleToTeam = "dtv_AddRoleToTeam";
        public const string dtv_AddRoleToUser = "dtv_AddRoleToUser";
        public const string dtv_AddUserToTeam = "dtv_AddUserToTeam";
        public const string dtv_CalculateRollupField = "dtv_CalculateRollupField";
        public const string dtv_CheckUserInRole = "dtv_CheckUserInRole";
        public const string dtv_CheckUserInTeam = "dtv_CheckUserInTeam";
        public const string dtv_EmailToTeam = "dtv_EmailToTeam";
        public const string dtv_GetEnvironmentVariable = "dtv_GetEnvironmentVariable";
        public const string dtv_RemoveRoleFromTeam = "dtv_RemoveRoleFromTeam";
        public const string dtv_RemoveRoleFromUser = "dtv_RemoveRoleFromUser";
        public const string dtv_RemoveUserFromTeam = "dtv_RemoveUserFromTeam";

    }


}
