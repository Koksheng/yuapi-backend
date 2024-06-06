-yuapi-backend
--yuapi.Api
--yuapi.Application
--yuapi.Contracts
--yuapi.Domain
--yuapi.Infrastructure

By organizing your code this way, you ensure that each layer has a specific responsibility, making your application easier to maintain, extend, and test. 

The controllers handle HTTP requests and responses, the application layer contains the business logic, the domain layer defines the core business entities and rules, and the infrastructure layer handles data persistence.