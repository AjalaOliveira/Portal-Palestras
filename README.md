# Portal-Palestras

1////
This application uses a instance of (localdb)\mssqllocaldb server.
For run using another DATA SOURCE it's necessary change it on following files:
  * Palestras.UI.Site\appsettings.json
  * Palestras.WebApi\appsettings.json
  * Palestras.Infra.Data\appsettings.json
  * Palestras.Infra.CrossCutting.Identity\appsettings.json

2////
After to define the DATA SOURCE it's necessary run Migration commands.

*** MIGRATION ***
USE DATA BASE FIRST:

Run te following commands by Packge Management Console from Visual Studio.

Project: 
* Palestras.Infra.CrossCutting.Identity

Commands: 
* update-database -Context IdentityDbContext

Project: 
* Palestras.Infra.Data

Commands: 
* update-database -Context PalestrasDbContext
* update-database -Context EventStoreSQLContext
