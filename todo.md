-yuapi-backend.sln
--src--yuapi.Api
--src--yuapi.Application
--src--yuapi.Contracts
--src--yuapi.Domain
--src--yuapi.Infrastructure

--yuapi-interface--yuapi-interface.sln
--yuapi-interface--yuapi-client-sdk
--yuapi-interface--yuapi-interface
--yuapi-interface--yuapi-interfce-console-app


By organizing your code this way, you ensure that each layer has a specific responsibility, making your application easier to maintain, extend, and test. 

The controllers handle HTTP requests and responses, the application layer contains the business logic, the domain layer defines the core business entities and rules, and the infrastructure layer handles data persistence.