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

## Special SQL Server Configuration

In case the database configured in the Web.config does not work for you as it is, please avoid modifying it. The problem is that the instance name differs from machine to machine but fixing it is very straight forward.

If you have Visual Studio 2012 or newer, then you have LocalDB installed already so the (localdb) part of the connection string should work. The problem might be the instance name. This name might be "Projects", "v11.0", "v12.0" and many others.

We are using the "Projects" instance name. If you don't have this instance running on your machine, create it and start it using the following commands on you CMD.

- You can see which instances you have by running the following:

```bat
sqllocaldb info
```

- If you don't already have it, create a new instance with the required name (the `-s` flag starts it):

```bat
sqllocaldb create "Projects" -s
```

- If for any reason the instance stops, start it again by running:

```bat
sqllocaldb start "Projects"
```