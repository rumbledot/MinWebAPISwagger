# Minimal WebAPI with Swagger and EFCore

An experimentation with .NET 6 Minimal WebAPI with Swagger and EFCore.

## Setup

1. New ASP.NET Core Web Api project
2. Uncheck <b>Use Controllers</b>
3. Setup a SQL Server database and table

## Structure

I like to keep Program.cs clean as possible.
Services, depedency injections, configuration and endpoints are moved to static classes.

### Folder Organization:

1. Data  
   Repository and mock data classes live here. Including EFCore DBContexts.

2. Endpoints  
   Minimal WebAPI endpoints are organized in different static classes.

3. Helpers  
   Handy static classes that help organize Program.cs

4. Services

5. Validation  
   Token service and validation injection live here.

## Nuget Package

1. EFCore
2. EFCore.SqlServer
3. FluentValidation
4. FluentValidation AspNetCore
5. FluentValisation DependencyInjectionExtension
6. Swashbuckle (automatically installed by template)
