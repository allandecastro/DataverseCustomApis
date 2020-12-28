# DataverseCustomApis

## Introduction

Following the latest announcements about the end of life of Custom Activities, Microsoft has announced a new feature called Custom API.
This is intended to replace Customs Activities but involves a few additional steps to introduce it, notably by creating a new type of solutio-aware component : Custom API, Custom API Request Parameter and Custom API Response Property.
If you want to understand how to create a Custom API you can refer to the following link:

### Disclaimer 
> Custom API functionality is still considered as a Preview feature. While unlikely, some breaking changes might occur and will be fixed ASAP.

Here is the link to the Dataverse Custom API's [official documentation](https://docs.microsoft.com/en-us/powerapps/developer/common-data-service/custom-api)

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

This custom API allows you to add a specific security role (using the name or reference to that security role) to a specific user (or to the InitiatingUser if the parameter is not met).</p>
           
InputParameters | Type | Optional
| :---: | :---: | :---:
RoleName  | String | ❌
Role  | EntityReference | ❌
TeamName  | String | ❌
Team  | EntityReference | ❌

</details>
<details>
             <summary>🆕 AddRoleToUser</summary>
           <p>GetEnvironmentVariable description</p>
</details>
<details>
             <summary>🆕 AddUserToTeam</summary>
           <p>ContentClone record</p>
</details>
<details>
             <summary>🆕 CalculateRollUpField</summary>
           <p>ContentClone record</p>
</details>
<details>
             <summary>🆕 CheckUserInRole</summary>
           <p>ContentClone record</p>
</details>
<details>
             <summary>🆕 CheckUserInTeam</summary>
           <p>ContentClone record</p>
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
### Calling Custom APIs from SDK
### Calling Custom APIs from WebAPI
### Calling Custom APIs from Power Automate (Cloud Flows)
