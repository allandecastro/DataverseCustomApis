# DataverseCustomApis

## Introduction

Following the latest announcements about the end of life of Custom Activities, Microsoft has announced a new feature called Custom API.
This is intended to replace Customs Activities but involves a few additional steps to introduce it, notably by creating a new type of solutio-aware component : Custom API, Custom API Request Parameter and Custom API Response Property.
If you want to understand how to create a Custom API you can refer to the following link:

## Project Description

Based on the legendary project of Demian Adolfo Raschkovan (https://github.com/demianrasko/Dynamics-365-Workflow-Tools), I wanted to use the same concept but with the latest features.
This project is intended to evolve!

### Project Installation

As discussed above, there are severals components to be installed/initiated if you want to use this new feature.
That the reason 

- - - -

### Custom APIs

Here is the complete list of Custom APIs included in this project:

<details>
           <summary>🆕 AddRoleToTeam</summary>
           <p>

This custom API allows you to add a specific security role (using the name or reference to that security role) to a specific Team.</p>

#### Custom API Definition

Unique Name | Name/Display Name |Binding Type |Bound Entity Logical Name | Is Function |Is Private | Allowed Custom Processing Step Type | Execute privilege Name 
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: 
dtv_AddRoleToTeam|dtv_AddRoleToTeam|Global|N/A|❌|❌|None|

#### Custom API Request Parameter(s)
Name | Type | Is Optional
| :---: | :---: | :---:
RoleName  | String | ❌
Role  | EntityReference (role) | ❌
TeamName  | String | ❌
Team  | EntityReference (team) | ❌

#### Custom API Response Property(ies)
N/A

</details>
<details>
             <summary>🆕 AddRoleToUser</summary>
           <p>

This custom API allows you to add a specific security role (using the name or the reference to that security role) to a specific user (or to the InitiatingUser if the parameter is not set).</p>
           
#### Custom API Definition

Unique Name | Name/Display Name |Binding Type |Bound Entity Logical Name | Is Function |Is Private | Allowed Custom Processing Step Type | Execute privilege Name 
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: 
dtv_AddRoleToUser|dtv_AddRoleToUser|Global|N/A|❌|❌|None|

#### Custom API Request Parameter(s)
Name | Type | Is Optional
| :---: | :---: | :---:
RoleName  | String | ❌
Role  | EntityReference (role) | ❌
User  | EntityReference (systemuser) | ❌

#### Custom API Response Property(ies)
N/A

</details>
<details>
             <summary>🆕 AddUserToTeam</summary>
           <p>
                      
This custom API allows you to add a specific user (or the InitiatingUser if the parameter is not set) to a specific Team (using the name or the reference to that team).</p>
           
#### Custom API Definition

Unique Name | Name/Display Name |Binding Type |Bound Entity Logical Name | Is Function |Is Private | Allowed Custom Processing Step Type | Execute privilege Name 
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: 
dtv_AddUserToTeam|dtv_AddUserToTeam|Global|N/A|❌|❌|None|

#### Custom API Request Parameter(s)
Name | Type | Is Optional
| :---: | :---: | :---:
TeamName  | String | ❌
Team  | EntityReference (team) | ❌
User  | EntityReference (systemuser) | ❌

#### Custom API Response Property(ies)
N/A

</details>
<details>
             <summary>🆕 CalculateRollUpField</summary>
           <p>
                      
This custom API allows you to force the Rollup field calculation for a specific record and field and return the result.</p>

#### Custom API Definition

Unique Name | Name/Display Name |Binding Type |Bound Entity Logical Name | Is Function |Is Private | Allowed Custom Processing Step Type | Execute privilege Name 
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: 
dtv_CalculateRollUpField|dtv_CalculateRollUpField|Global|N/A|❌|❌|None|

#### Custom API Request Parameter(s)
Name | Type | Is Optional
| :---: | :---: | :---:
FieldName  | String | ❌
Entity  | Entity (team) | ❌
User  | EntityReference (systemuser) | ❌

-->Vérifier le paramètre Entity et si on fait bound ou pas 

#### Custom API Response Property(ies)
Name | Type 
| :---: | :---: 
MoneyValue | Money
DateTimeValue | DateTime
WholeNumberValue | Integer
DecimalValue | Decimal

</details>
<details>
             <summary>🆕 CheckUserInRole</summary>
                      <p>
                      
This custom API allows you to check if a specific user (or the InitiatingUser if the parameter is not set) has the role defined in parameter (using the name or the reference to that security role).</p>

#### Custom API Definition

Unique Name | Name/Display Name |Binding Type |Bound Entity Logical Name | Is Function |Is Private | Allowed Custom Processing Step Type | Execute privilege Name 
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: 
dtv_CheckUserInRole|dtv_CheckUserInRole|Global|N/A|❌|❌|None|

#### Custom API Request Parameter(s)
Name | Type | Is Optional
| :---: | :---: | :---:
RoleName  | String | ❌
Role  | EntityReference (role) | ❌
User  | EntityReference (systemuser) | ❌

-->Vérifier l'aspect Function/Action, ici on devrait faire une fonction car on altère pas la data 

#### Custom API Response Property(ies)
Name | Type 
| :---: | :---: 
IsUserInRole | Boolean

</details>
<details>
             <summary>🆕 CheckUserInTeam</summary>
                      <p>
                      
This custom API allows you to check if a specific user (or the InitiatingUser if the parameter is not set) belongs to the team defined in parameter (using the name or the reference to that security role).</p>

#### Custom API Definition

Unique Name | Name/Display Name |Binding Type |Bound Entity Logical Name | Is Function |Is Private | Allowed Custom Processing Step Type | Execute privilege Name 
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: 
dtv_CheckUserInTeam|dtv_CheckUserInTeam|Global|N/A|❌|❌|None|

#### Custom API Request Parameter(s)
Name | Type | Is Optional
| :---: | :---: | :---:
TeamName  | String | ❌
Team  | EntityReference (team) | ❌
User  | EntityReference (systemuser) | ❌

-->Vérifier l'aspect Function/Action, ici on devrait faire une fonction car on altère pas la data 

#### Custom API Response Property(ies)
Name | Type 
| :---: | :---: 
IsUserInTeam | Boolean

</details>
<details>
             <summary>🆕 EmailToTeam</summary>
           <p>ContentClone record</p>
</details>
<details>
             <summary>🆕 GetEnvironmentVariable</summary>
           <p>ContentClone record</p>
</details>
<details>
             <summary>🆕 RemoveRoleFromTeam</summary>
           <p>ContentClone record</p>
</details>
<details>
             <summary>🆕 RemoveRoleFromUser</summary>
           <p>ContentClone record</p>
</details>
<details>
             <summary>🆕 RemoveUserFromTeam</summary>
           <p>ContentClone record</p>
</details>

### Calling Custom APIs from JavaScript

Request:

```javascript
 function RunAction() {
var parameters = {};
parameters.FieldName = "adc_rollupdecimal";
var entity = {};
entity.accountid = "95559db1-710c-eb11-a816-002248049f5d"; 
entity["@odata.type"] = "Microsoft.Dynamics.CRM.account";
parameters.Entity = entity;

var dtv_CalculateRollupFieldRequest = {
    FieldName: parameters.FieldName,
    Entity: parameters.Entity,

    getMetadata: function() {
        return {
            boundParameter: null,
            parameterTypes: {
                "FieldName": {
                    "typeName": "Edm.String",
                    "structuralProperty": 1
                },
                "Entity": {
                    "typeName": "mscrm.crmbaseentity",
                    "structuralProperty": 5
                }
            },
            operationType: 0,
            operationName: "dtv_CalculateRollupField"
        };
    }
};

Xrm.WebApi.online.execute(dtv_CalculateRollupFieldRequest).then(
    function success(result) {
        if (result.ok) {
            var results = JSON.parse(result.responseText);
        }
    },
    function(error) {
        Xrm.Utility.alertDialog(error.message);
    }
);
 }
```

Response:
```json
{

    "@odata.context":"https://org.crm.dynamics.com/api/data/v9.0/$metadata#Microsoft.Dynamics.CRM.dtv_CalculateRollupFieldResponse",
    "MoneyValue": null ,
    "DateTimeValue": null ,
    "WholeNumberValue": null ,
    "DecimalValue":35.67

}
```
### Calling Custom APIs from SDK


```csharp
OrganizationRequest request = new OrganizationRequest("dtv_AddRoleToUser")
{
  ["Role"] = new EntityReference("role", new Guid("182580da-7ccc-e911-a813-000d3a7ed5a2")),
  ["User"] = new EntityReference("systemuser", new Guid("794580da-7ccc-e911-a813-000d3a7ed5a2")),
};
OrganizationResponse response = svc.Execute(request);
```

### Calling Custom APIs from WebAPI

Request:
```
https://pcfinstance.crm.dynamics.com/api/data/v9.0/dtv_CalculateRollupField()
```

Request Body:
```json
{
    "FieldName":"adc_rollupcurrency",
    "Entity":{
        "accountid":"95559db1-710c-eb11-a816-002248049f5d",
        "@odata.type":"Microsoft.Dynamics.CRM.account"
    }
}
```

Response:
```json
{
    "@odata.context": "https://org.crm.dynamics.com/api/data/v9.0/$metadata#Microsoft.Dynamics.CRM.dtv_CalculateRollupFieldResponse",
    "MoneyValue": 16000.0000000000,
    "DateTimeValue": null,
    "WholeNumberValue": null,
    "DecimalValue": null
}
```

### Calling Custom APIs from Power Automate (Cloud Flows)


### Disclaimer 
> Custom API functionality is still considered as a Preview feature. While unlikely, some breaking changes might occur and will be fixed ASAP.

Here is the link to the Dataverse Custom API's [official documentation](https://docs.microsoft.com/en-us/powerapps/developer/common-data-service/custom-api)
