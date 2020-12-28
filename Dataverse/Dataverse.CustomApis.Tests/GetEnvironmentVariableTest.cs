using FakeXrmEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;

namespace Dataverse.CustomApis.Tests
{
    [TestClass]
    public class GetEnvironmentVariableTest
    {
        [TestMethod]
        public void CheckStringEnvironmentValue()
        {
            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity environnementVariable = new Entity("environmentvariabledefinition", Guid.NewGuid());
            environnementVariable["schemaname"] = "dtv_VarTest";
            environnementVariable["valueschema"] = "";
            environnementVariable["displayname"] = "String";
            environnementVariable["type"] = new OptionSetValue(100000000);
            environnementVariable["defaultvalue"] = "";


            Entity environmentVariableValue = new Entity("environmentvariablevalue", Guid.NewGuid());
            environmentVariableValue["environmentvariabledefinitionid"] = environnementVariable.ToEntityReference();
            environmentVariableValue["value"] = "DEVURL";

            fakeContext.Initialize(new List<Entity>() {
                environnementVariable,
                environmentVariableValue
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
                MessageName = "GetEnvironmentVariable",
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

        [TestMethod]
        public void CheckBooleanEnvironmentValue()
        {

            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity environnementVariable = new Entity("environmentvariabledefinition", Guid.NewGuid());
            environnementVariable["schemaname"] = "dtv_Boolean";
            environnementVariable["valueschema"] = "";
            environnementVariable["displayname"] = "Boolean";
            environnementVariable["type"] = new OptionSetValue(100000002);
            environnementVariable["defaultvalue"] = "no";


            Entity environmentVariableValue = new Entity("environmentvariablevalue", Guid.NewGuid());
            environmentVariableValue["environmentvariabledefinitionid"] = environnementVariable.ToEntityReference();
            environmentVariableValue["value"] = "true";

            fakeContext.Initialize(new List<Entity>() {
                environnementVariable,
                environmentVariableValue
            });

            ParameterCollection inputparameters = new ParameterCollection
            {
                { "Key", "dtv_Boolean" }
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
                MessageName = "GetEnvironmentVariable",
                InputParameters = inputparameters,
                OutputParameters = outputparameters,
                Stage = 30
            };

            fakeContext.ExecutePluginWith<GetEnvironmentVariable>(context);
            #endregion

            #region ASSERT
            Assert.IsNull(context.OutputParameters["StringValue"]);
            Assert.IsNotNull(context.OutputParameters["BooleanValue"]);
            Assert.IsNull(context.OutputParameters["DecimalValue"]);
            #endregion
        }

        [TestMethod]
        public void CheckDecimalEnvironmentValue()
        {

            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity environnementVariable = new Entity("environmentvariabledefinition", Guid.NewGuid());
            environnementVariable["schemaname"] = "dtv_Number";
            environnementVariable["valueschema"] = "";
            environnementVariable["displayname"] = "Decimal";
            environnementVariable["type"] = new OptionSetValue(100000001);
            environnementVariable["defaultvalue"] = 5.678;


            Entity environmentVariableValue = new Entity("environmentvariablevalue", Guid.NewGuid());
            environmentVariableValue["environmentvariabledefinitionid"] = environnementVariable.ToEntityReference();
            environmentVariableValue["value"] = 10.6789;

            fakeContext.Initialize(new List<Entity>() {
                environnementVariable,
                environmentVariableValue
            });

            ParameterCollection inputparameters = new ParameterCollection
            {
                { "Key", "dtv_Number" }
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
                MessageName = "GetEnvironmentVariable",
                InputParameters = inputparameters,
                OutputParameters = outputparameters,
                Stage = 30
            };

            fakeContext.ExecutePluginWith<GetEnvironmentVariable>(context);
            #endregion

            #region ASSERT
            Assert.IsNull(context.OutputParameters["StringValue"]);
            Assert.IsNull(context.OutputParameters["BooleanValue"]);
            Assert.IsNotNull(context.OutputParameters["DecimalValue"]);
            #endregion
        }
        [TestMethod]
        public void CheckWholeNumberEnvironmentValue()
        {

            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity environnementVariable = new Entity("environmentvariabledefinition", Guid.NewGuid());
            environnementVariable["schemaname"] = "dtv_WholeNumber";
            environnementVariable["valueschema"] = "";
            environnementVariable["displayname"] = "wholeNumber";
            environnementVariable["type"] = new OptionSetValue(100000001);
            environnementVariable["defaultvalue"] = 5.678;


            Entity environmentVariableValue = new Entity("environmentvariablevalue", Guid.NewGuid());
            environmentVariableValue["environmentvariabledefinitionid"] = environnementVariable.ToEntityReference();
            environmentVariableValue["value"] = 10.6789;

            fakeContext.Initialize(new List<Entity>() {
                environnementVariable,
                environmentVariableValue
            });

            ParameterCollection inputparameters = new ParameterCollection
            {
                { "Key", "dtv_WholeNumber" }
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
                MessageName = "GetEnvironmentVariable",
                InputParameters = inputparameters,
                OutputParameters = outputparameters,
                Stage = 30
            };

            fakeContext.ExecutePluginWith<GetEnvironmentVariable>(context);
            #endregion

            #region ASSERT
            Assert.IsNull(context.OutputParameters["StringValue"]);
            Assert.IsNull(context.OutputParameters["BooleanValue"]);
            Assert.IsNotNull(context.OutputParameters["DecimalValue"]);
            #endregion
        }
        [TestMethod]
        public void CheckJsonEnvironmentValue()
        {
            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity environnementVariable = new Entity("environmentvariabledefinition", Guid.NewGuid());
            environnementVariable["schemaname"] = "dtv_Json";
            environnementVariable["valueschema"] = "";
            environnementVariable["displayname"] = "Json";
            environnementVariable["type"] = new OptionSetValue(100000003);
            environnementVariable["defaultvalue"] = "";


            Entity environmentVariableValue = new Entity("environmentvariablevalue", Guid.NewGuid());
            environmentVariableValue["environmentvariabledefinitionid"] = environnementVariable.ToEntityReference();
            environmentVariableValue["value"] = "{\"Name\":\"Allan\"}";

            fakeContext.Initialize(new List<Entity>() {
                environnementVariable,
                environmentVariableValue
            });

            ParameterCollection inputparameters = new ParameterCollection
            {
                { "Key", "dtv_Json" }
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
                MessageName = "GetEnvironmentVariable",
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
        [TestMethod]
        public void CheckInputParameterConsistent()
        {
            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity environnementVariable = new Entity("environmentvariabledefinition", Guid.NewGuid());
            environnementVariable["schemaname"] = "dtv_Random";
            environnementVariable["valueschema"] = "";
            environnementVariable["displayname"] = "String";
            environnementVariable["type"] = new OptionSetValue(100000000);
            environnementVariable["defaultvalue"] = "";


            Entity environmentVariableValue = new Entity("environmentvariablevalue", Guid.NewGuid());
            environmentVariableValue["environmentvariabledefinitionid"] = environnementVariable.ToEntityReference();
            environmentVariableValue["value"] = "DEVURL";

            fakeContext.Initialize(new List<Entity>() {
                environnementVariable,
                environmentVariableValue
            });

            ParameterCollection inputparameters = new ParameterCollection
            {

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
                MessageName = "GetEnvironmentVariable",
                InputParameters = inputparameters,
                OutputParameters = outputparameters,
                Stage = 30
            };


            #endregion

            #region ASSERT
            Assert.ThrowsException<InvalidPluginExecutionException>(() => fakeContext.ExecutePluginWith<GetEnvironmentVariable>(context));
            #endregion
        }
        [TestMethod]
        public void CheckEnvironmentVariableExists()
        {
            #region ARRANGE
            XrmFakedContext fakeContext = new XrmFakedContext();

            Entity environnementVariable = new Entity("environmentvariabledefinition", Guid.NewGuid());
            environnementVariable["schemaname"] = "dtv_Random";
            environnementVariable["valueschema"] = "";
            environnementVariable["displayname"] = "String";
            environnementVariable["type"] = new OptionSetValue(100000000);
            environnementVariable["defaultvalue"] = "";


            Entity environmentVariableValue = new Entity("environmentvariablevalue", Guid.NewGuid());
            environmentVariableValue["environmentvariabledefinitionid"] = environnementVariable.ToEntityReference();
            environmentVariableValue["value"] = "DEVURL";

            fakeContext.Initialize(new List<Entity>() {
                environnementVariable,
                environmentVariableValue
            });

            ParameterCollection inputparameters = new ParameterCollection
            {
                { "Key", "dtv_UnknowKey" }
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
                MessageName = "GetEnvironmentVariable",
                InputParameters = inputparameters,
                OutputParameters = outputparameters,
                Stage = 30
            };


            #endregion

            #region ASSERT
            Assert.ThrowsException<InvalidPluginExecutionException>(() => fakeContext.ExecutePluginWith<GetEnvironmentVariable>(context));
            #endregion
        }
    }
}
