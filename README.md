# Portal-Palestras

 *** MIGRATION ***
 
USE DATABASE FIRST:

Project: 
* Palestras.Infra.CrossCutting.Identity

Commands: 
* add-migration -Context IdentityDbContext Initial.
* update-database -Context IdentityDbContext

Project: 
* Palestras.Infra.CrossCutting.Identity

Commands: 
* add-migration -Context PalestrasDbContext Initial
* update-database -Context PalestrasDbContext
* add-migration -Context EventStoreSQLContext Initial
* update-database -Context EventStoreSQLContext

DATA SOURCE - SQL: (localdb)\mssqllocaldb

DATABASE: Palestras

For use another DATA SOURCE is necessary change it on following files:
  * Palestras.UI.Site\appsettings.json
  * Palestras.WebApi\appsettings.json
  * Palestras.Infra.Data\appsettings.json
  * Palestras.Infra.CrossCutting.Identity\appsettings.json
