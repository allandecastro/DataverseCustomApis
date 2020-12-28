using FakeXrmEasy;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Dataverse.CustomApis.Tests
{
    [TestClass]
    public class CalculateRollupFieldTest
    {
        [TestMethod]
        public void CheckExecution()
        {
            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity account = new Entity("account")
            {
                ["accountid"] = Guid.NewGuid(),
                ["dtv_actualamount"] = new Money(123)
            };

            fakeContext.AddExecutionMock<CalculateRollupFieldRequest>(request =>
            {
                var req = request as CalculateRollupFieldRequest;


                var results = new ParameterCollection { { "Entity", account } };
                var response = new CalculateRollupFieldResponse()
                {
                    ResponseName = "CalculateRollupField",
                    Results = results
                };
                return response;
            });

            fakeContext.Initialize(new List<Entity>() {
     

            });

            ParameterCollection inputparameters = new ParameterCollection
            {
                { "Key", "dtv_VarTest" }
            };


            ParameterCollection outputparameters = new ParameterCollection
            {
                { "StringValue", null },
                { "BooleanValue", null },
                { "DecimalValue", null }
            };
            #endregion

            #region ACT
            XrmFakedPluginExecutionContext context = new XrmFakedPluginExecutionContext
            {
                MessageName = "dtv_GetEnvironmentVariable",
                InputParameters = inputparameters,
                OutputParameters = outputparameters,
                Stage = 30
            };

            fakeContext.ExecutePluginWith<GetEnvironmentVariable>(context);
            #endregion

            #region ASSERT
            Assert.IsNotNull(context.OutputParameters["StringValue"]);
            Assert.IsNull(context.OutputParameters["BooleanValue"]);
            Assert.IsNull(context.OutputParameters["DecimalValue"]);
            #endregion
        }

    }
}
