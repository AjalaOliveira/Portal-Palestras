# Portal-Palestras

 *** MIGRATION ***
 
USE DATA BASE FIRST:

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

DATA BASE: Palestras

For use another DATA SOURCE it's necessary change it on following files:
  * Palestras.UI.Site\appsettings.json
  * Palestras.WebApi\appsettings.json
  * Palestras.Infra.Data\appsettings.json
  * Palestras.Infra.CrossCutting.Identity\appsettings.json
  
After register a new User it's neccessary to define some CLAIMS in dbo.AspNetUserClaims Table:
  * CanWritePalestranteData => ClaimsType: Palestrantes - Value: Write (defined by default)
  * CanRemovePalestranteData => ClaimsType: Palestrantes - Value: Remove
  * CanWritePalestraData => ClaimsType: Palestras - Value: Write
  * CanRemovePalestraData => ClaimsType: Palestras - Value: Remove


