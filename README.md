# WebApi_with_serialization
Simple WebApi which allows saving records to files in XML format and handles Requests in JSON format.

# Features:
1. Ability to store db records in xml files.
2. Integration of console app with WebApi - creating records by sending requests using HttpClient.
3. XML transformation of config files.
4. Swagger auto-documenting based on XML comments.
5. Logging to files / console window / smtp (if configured) using log4net.
6. Automatic database migrations.
7. Some design and architectural patterns implemented, like Repository, UnifOfWork, Facade etc.
8. Fully asynchronous code for scalability (async all the way).

# Main technologies:
* .NET Framework 4.7.2 / C#
* WebApi2
* Swagger
* Autofac
* log4net
* xUnit
* Bogus Faker
* NSubstitute
* EntityFramework 6
