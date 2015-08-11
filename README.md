# ProCultura HN

API for ProCultura Project

## Project Setup for local development

- Create database with the same name as the specified in the Web.Config file. i.e. `ProCulturaBackenDB`
- Create the Localization schema and tables using the script located in `ProCultura.Data/Scripts`
- The built-in SQL Server Manager in Visual Studio may be used through the SQL Server Object Explorer
- Scripts must be run in the following order:
    - LocalizationTablesCreateScript.sql
    - LanguageSeedScript.sql
    - DictionaryEntriesSeedScript.sql

## Running the project from the command line
The following code snippet might be useful to run the project without opening Visual Studio.

```bat
"C:\Program Files (x86)\Common Files\Microsoft Shared\DevServer\11.0\WebDev.WebServer40.EXE" /path:"C:\Path\To\Repository\ProCulturaBackend\ProCultura.Web.Api" /port:11705
```